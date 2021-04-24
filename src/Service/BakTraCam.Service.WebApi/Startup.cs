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
using Microsoft.EntityFrameworkCore;

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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env )
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
        }
    }
}
