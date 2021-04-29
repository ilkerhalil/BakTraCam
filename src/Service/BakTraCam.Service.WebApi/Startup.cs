using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BakTraCam.Core.Business.Application.Common;
using BakTraCam.Core.Business.Application.Maintenance;
using BakTraCam.Core.Business.Application.User;
using BakTraCam.Core.Business.Domain.Common;
using BakTraCam.Core.Business.Domain.Maintenance;
using BakTraCam.Core.Business.Domain.User;
using BakTraCam.Core.DataAccess.Context;
using BakTraCam.Core.DataAccess.Repositories.Maintenance;
using BakTraCam.Core.DataAccess.Repositories.Notice;
using BakTraCam.Core.DataAccess.Repositories.User;
using BakTraCam.Core.DataAccess.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BakTraCam.Util.Mapping.Adapter;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BakTraCam.Service.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        private IConfiguration Configuration { get; }

        private readonly Logger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostEnvironment"></param>
        public Startup(IHostEnvironment hostEnvironment)
        {
            var applicationApplicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(applicationApplicationBasePath)
                .AddJsonFile("appsettings.json", true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console()
                .Enrich.WithExceptionDetails()
                .CreateLogger();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMemoryCache();
            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddNewtonsoftJson(options =>
            {
                options.UseMemberCasing();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

            services
                .AddScoped<IMaintenanceApplication, MaintenanceApplication>();
            services
                .AddScoped<IUserApplication, UserApplication>();
            services
                .AddScoped<ICommonApplication, CommonApplication>();
            services
                .AddScoped<IMaintenanceDomain, MaintenanceDomain>();
            services
                .AddScoped<IUserDomain, UserDomain>();
            services
                .AddScoped<ICommonDomain, CommonDomain>();
            services
                .AddScoped<IDatabaseUnitOfWork, DatabaseUnitOfWork>();
            services
                .AddScoped<ICustomMapper, CustomMapper>();
            services
                .AddScoped<IMaintenanceRepository, MaintenanceRepository>();
            services
                .AddScoped<IUserRepository, UserRepository>();
            services
                .AddScoped<INoticeRepository, NoticeRepository>();

            services
                .AddLogging(cfg => cfg.AddSerilog(_logger));

            services.AddSingleton<ICustomMapper>(new CustomMapper());
            var connectionString = Configuration.GetConnectionString("mainDb");
            services
                .AddDbContext<BakTraCamContext>(opt => opt.UseSqlite(connectionString));

            //services.AddDbContextEnsureCreatedMigrate<DatabaseContext>
            //    (options => options.UseSqlite(connectionString));

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                opt.SetDefaultCulture("tr-TR");
            });
            services
                .AddApiVersioning(cfg =>
                {
                    cfg.DefaultApiVersion = new ApiVersion(1, 0);
                    cfg.AssumeDefaultVersionWhenUnspecified = true;
                    cfg.ReportApiVersions = true;
                });

            services
                .AddVersionedApiExplorer(opt =>
                {
                    opt.GroupNameFormat = "'v'VVV";
                    opt.SubstituteApiVersionInUrl = true;
                });

            services
                .AddSwaggerGen(c =>
                {
                    var provider = services.BuildServiceProvider();
                    var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in service.ApiVersionDescriptions)
                    {

                        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        c.IncludeXmlComments(xmlPath);
                        c.CustomSchemaIds(x => x.FullName);
                        c.SwaggerDoc(description.GroupName, CreateMetaInfoApiVersion(description));
                    }

                });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var sirketId = Configuration.GetSection("AppParameters").GetValue<int>("SirketId");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            var clientApps = Configuration
                .GetSection("AppParameters")
                .GetSection("ClientApps")
                .Get<string[]>();
            app.UseCors(builder => builder
                            .WithOrigins(clientApps)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(ep =>
            {
                ep.MapControllers();
            });
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.DisplayRequestDuration();
                var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                foreach (var apiVersionDescription in provider
                    .ApiVersionDescriptions
                    .OrderByDescending(x => x.ApiVersion))
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                        $"Version {apiVersionDescription.ApiVersion}");
                }
            });
            app.UseHealthChecks("/healthz", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = WriteResponse,
                ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status500InternalServerError
                    }
            });

        }
        private OpenApiInfo CreateMetaInfoApiVersion(ApiVersionDescription description)
        {
            return new()
            {
                Title = "BakTraCam",
                Version = description.ApiVersion.ToString(),
                Description = " This service is TEST sample service which provides ability to get weather control data ",
            };
        }
        private Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var options = new JsonWriterOptions()
            {
                Indented = true
            };
            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream, options))
            {
                writer.WriteStartObject();
                writer.WriteString("status",result.Status.ToString());
                writer.WriteStartObject("results");
                foreach (var healthReportEntry in result.Entries)
                {
                    writer.WriteStartObject(healthReportEntry.Key);
                    writer.WriteString("status",healthReportEntry.Value.Status.ToString());
                    writer.WriteString("description",healthReportEntry.Value.Description);
                    writer.WriteStartObject("data");
                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        writer.WritePropertyName(item.Key);
                        JsonSerializer.Serialize(writer, item.Value, item.Value?.GetType() ?? typeof(object));
                    }
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            var json = Encoding.UTF8.GetString(stream.ToArray());
            return context.Response.WriteAsync(json);
        }
    }
}
