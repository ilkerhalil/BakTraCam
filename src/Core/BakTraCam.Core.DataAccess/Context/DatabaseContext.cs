using BakTraCam.Core.Entity;
using BakTraCam.Service.DataContract;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace BakTraCam.Core.DataAccess.Context
{
    public class BakTraCamContext : DbContext
    {
        public BakTraCamContext(DbContextOptions options) : base(options)
        {

        }

        DbSet<MaintenanceEntity> Maintenance { get; set; }
        DbSet<NoticeEntity> Notice { get; set; }
        DbSet<UserEntity> User { get; set; }

        [IgnoreDataMember]
        DbSet<MaintenanceModelBasic> Bakims { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SelectModel>().HasNoKey().ToView("SelectModel");
        }
    }
}
