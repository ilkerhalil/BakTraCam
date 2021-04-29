using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Domain.Common
{
    public interface ICommonDomain
    {
        Task<IEnumerable<SelectModel>> KullanicilariGetirAsync();
        Task<IEnumerable<NoticeModel>> DuyurulariGetirAsync();
        Task<NoticeModel> KaydetDuyuruAsync(NoticeModel model);
        Task<int> SilDuyuruAsync(int id);
        Task<NoticeModel> DuyuruGetirAsync(int id);
    }
}
