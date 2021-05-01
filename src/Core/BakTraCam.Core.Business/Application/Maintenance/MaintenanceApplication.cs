using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Core.Business.Domain.Maintenance;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.Maintenance
{
    public class MaintenanceApplication : ApplicationBase<MaintenanceApplication>, IMaintenanceApplication
    {
        private readonly IMaintenanceDomain _maintenanceDomain;

 
        public MaintenanceApplication(IDatabaseUnitOfWork uow,IMaintenanceDomain maintenanceDomain) 
            : base(uow)
        {
            _maintenanceDomain = maintenanceDomain;
        }

        public Task<IEnumerable<MaintenanceModelBasic>> OnBesGunYaklasanBakimlariGetirAsync()
        {
            return _maintenanceDomain.OnBesGunYaklasanBakimlariGetirAsync();
        }
        public Task<IEnumerable<MaintenanceModelBasic>> BakimlistesiGetirAsync()
        {
            return _maintenanceDomain.BakimlariGetirAsync();
        }

        public Task<MaintenanceModel> GetirBakimAsync(int id)
        {
            return _maintenanceDomain.GetirBakimAsync(id);
        }

        public Task<IEnumerable<MaintenanceModelBasic>> GetirBakimListesiTipFiltreliAsync(int tip)
        {
            return _maintenanceDomain.getirBakimListesiTipFiltreliAsync(tip);
        }
        public Task<IEnumerable<MaintenanceModelBasic>> GetirBakimListesiDurumFiltreliAsync(int durum)
        {
            return _maintenanceDomain.getirBakimListesiDurumFiltreliAsync(durum);
        }

        public Task<MaintenanceModel> KaydetBakimAsync(MaintenanceModel bakimModel)
        {
            return _maintenanceDomain.KaydetBakimAsync(bakimModel);
        }
        public Task<int> SilBakimAsync(int id)
        {
            return _maintenanceDomain.SilBakimAsync(id);
        }

        
    }
}
