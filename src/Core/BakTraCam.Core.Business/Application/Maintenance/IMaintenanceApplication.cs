using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.Maintenance
{
    public interface IMaintenanceApplication
    {
        Task<IEnumerable<MaintenanceModelBasic>> OnBesGunYaklasanBakimlariGetirAsync();
        Task<IEnumerable<MaintenanceModelBasic>> BakimlistesiGetirAsync();
        Task<IEnumerable<MaintenanceModelBasic>> GetirBakimListesiTipFiltreliAsync(int tip);
        Task<IEnumerable<MaintenanceModelBasic>> GetirBakimListesiDurumFiltreliAsync(int durum);

        Task<MaintenanceModel> KaydetBakimAsync(MaintenanceModel bakimModel);
        Task<MaintenanceModel> GetirBakimAsync(int id);
        Task<int> SilBakimAsync(int id);
    }
}
