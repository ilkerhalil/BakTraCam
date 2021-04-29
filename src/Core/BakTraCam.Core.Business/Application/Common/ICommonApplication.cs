using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.Common
{
    public interface ICommonApplication
    {
        Task<IEnumerable<SelectModel>> KullaniciListesiGetirAsync();
        Task<IEnumerable<NoticeModel>> DuyuruListesiGetirAsync();
        Task<NoticeModel> KaydetDuyuruAsync(NoticeModel model);
        Task<int> SilDuyuruAsync(int id);
        Task<NoticeModel> DuyuruGetirAsync(int id);
    }
}
