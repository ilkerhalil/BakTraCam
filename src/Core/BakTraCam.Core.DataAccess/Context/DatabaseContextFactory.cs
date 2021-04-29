using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BakTraCam.Core.DataAccess.Context
{
    public class DatabaseContextFactory: IDesignTimeDbContextFactory<BakTraCamContext>
    {
        private IConfiguration Configuration { get; }
        public DatabaseContextFactory() { }
        public DatabaseContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public BakTraCamContext CreateDbContext(string[] args)
        {
            //var dbName = args.FirstOrDefault();

            var connectionString = Configuration?.GetConnectionString("mainDb") ?? "Datasource=BakTraCam.db";
            var builder = new DbContextOptionsBuilder<BakTraCamContext>();
            builder.UseSqlite(connectionString, options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
            return new BakTraCamContext(builder.Options);
        }
    }
}