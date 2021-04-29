using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Core.Business.Domain.Maintenance;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.Maintenance
{
    public class MaintenanceApplication : ApplicationBase<MaintenanceApplication>, IMaintenanceApplication
    {
        private readonly IMaintenanceDomain _bakimDom;

        //public BakimApplication(IServiceProvider serviceProvider) : base(serviceProvider)
        //{
        //    _bakimDom = serviceProvider.GetService<IBakimDomain>();
        //}
        public MaintenanceApplication(IDatabaseUnitOfWork uow,IMaintenanceDomain bakimDom) : base(uow)
        {
            _bakimDom = bakimDom;
        }

        public Task<IEnumerable<MaintenanceModelBasic>> OnBesGunYaklasanBakimlariGetirAsync()
        {
            return _bakimDom.OnBesGunYaklasanBakimlariGetirAsync();
        }
        public Task<IEnumerable<MaintenanceModelBasic>> BakimlistesiGetirAsync()
        {
            return _bakimDom.BakimlariGetirAsync();
        }

        public Task<MaintenanceModel> GetirBakimAsync(int id)
        {
            return _bakimDom.GetirBakimAsync(id);
        }

        public Task<IEnumerable<MaintenanceModelBasic>> GetirBakimListesiTipFiltreliAsync(int tip)
        {
            return _bakimDom.getirBakimListesiTipFiltreliAsync(tip);
        }
        public Task<IEnumerable<MaintenanceModelBasic>> GetirBakimListesiDurumFiltreliAsync(int durum)
        {
            return _bakimDom.getirBakimListesiDurumFiltreliAsync(durum);
        }

        public Task<MaintenanceModel> KaydetBakimAsync(MaintenanceModel bakimModel)
        {
            return _bakimDom.KaydetBakimAsync(bakimModel);
        }
        public Task<int> SilBakimAsync(int id)
        {
            return _bakimDom.SilBakimAsync(id);
        }

        
    }
}
