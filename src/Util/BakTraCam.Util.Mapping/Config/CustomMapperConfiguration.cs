using AgileObjects.AgileMapper.Configuration;
using BakTraCam.Common.Helper.Attributes;
using BakTraCam.Core.Entity;
using BakTraCam.Service.DataContract;


namespace BakTraCam.Util.Mapping.Config
{
    internal class CustomMapperConfiguration : MapperConfiguration
    {
        protected override void Configure()
        {
            MapMaintenance();
            MapCommon();
        }

        private void MapCommon()
        {
            WhenMapping
                .From<UserEntity>()
                .To<SelectModel>()
                //.Map((e, dto) => e.Name)
                //.To(dto => dto.Name)
                .And
                .Map((e, dto) => e.Id)
                .To(dto => dto.Key)
                ;

            GetPlansFor<UserEntity>().To<SelectModel>();
          
        }

        private void MapMaintenance()
        {
            WhenMapping
                    .From<MaintenanceEntity>()
                    .To<MaintenanceModel>()
                    .IgnoreTargetMembersWhere(m => m.HasAttribute<IgnoreMappingAttribute>());
            GetPlansFor<MaintenanceEntity>().To<MaintenanceModel>();
        }
        
    }
}