using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using BakTraCam.Core.Business.Application.Bakim;
using BakTraCam.Core.Business.Application.Kullanici;
using BakTraCam.Core.Business.Application.Ortak;
using BakTraCam.Core.Business.Domain.Bakim;
using BakTraCam.Core.Business.Domain.Kullanici;
using BakTraCam.Core.Business.Domain.Ortak;
using BakTraCam.Core.DataAccess.Context;
using BakTraCam.Core.DataAccess.Repositores.Bakim;
using BakTraCam.Core.DataAccess.Repositores.Duyuru;
using BakTraCam.Core.DataAccess.Repositores.Kullanici;
using BakTraCam.Core.DataAccess.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BakTraCam.Service.WebApi.Helpers;
using BakTraCam.Util.Mapping.Adapter;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BakTraCam.Service.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMemoryCache();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.UseMemberCasing();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services
                .AddScoped<IBakimApplication, BakimApplication>();
            services
                .AddScoped<IKullaniciApplication, KullaniciApplication>();
            services
                .AddScoped<IOrtakApplication, OrtakApplication>();
            services
                .AddScoped<IBakimDomain, BakimDomain>();
            services
                .AddScoped<IKullaniciDomain, KullaniciDomain>();
            services
                .AddScoped<IOrtakDomain, OrtakDomain>();
            services
                .AddScoped<IDatabaseUnitOfWork, DatabaseUnitOfWork>();
            services
                .AddScoped<ICustomMapper, CustomMapper>();
            services
                .AddScoped<IBakimRepository, BakimRepository>();
            services
                .AddScoped<IKullaniciRepository, KullaniciRepository>();
            services
                .AddScoped<IDuyuruRepository, DuyuruRepository>();



            services.AddSingleton<ICustomMapper>(new CustomMapper());
            var connectionString = Configuration.GetConnectionString("mainDb");
            services
                .AddDbContext<DatabaseContext>(opt => opt.UseSqlite(connectionString));
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
                    foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
                    {
                        
                        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        c.IncludeXmlComments(xmlPath);
                        c.CustomSchemaIds(x=> x.FullName);
                        c.SwaggerDoc(description.GroupName,CreateMetaInfoApiVersion(description));
                    }

                });
        }

        private OpenApiInfo CreateMetaInfoApiVersion(ApiVersionDescription description)
        {
            return new OpenApiInfo
            {
                Title = "BakTraCam",
                Version = description.ApiVersion.ToString(),
                Description = " This service is TEST sample service which provides ability to get weather control data ",
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
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
                
        }
    }
}
