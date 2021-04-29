using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Domain.Maintenance
{
    public interface IMaintenanceDomain
    {
        Task<IEnumerable<MaintenanceModelBasic>> OnBesGunYaklasanBakimlariGetirAsync();
        Task<IEnumerable<MaintenanceModelBasic>> BakimlariGetirAsync();
        Task<IEnumerable<MaintenanceModelBasic>> getirBakimListesiTipFiltreliAsync(int tip);
        Task<IEnumerable<MaintenanceModelBasic>> getirBakimListesiDurumFiltreliAsync(int durum);

        Task<MaintenanceModel> KaydetBakimAsync(MaintenanceModel model);
        Task<int> SilBakimAsync(int id);
        Task<MaintenanceModel> GetirBakimAsync(int id);
    }
}
