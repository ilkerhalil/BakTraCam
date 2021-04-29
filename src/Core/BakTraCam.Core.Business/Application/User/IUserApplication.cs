using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.User
{
    public interface IUserApplication
    {
        Task<IEnumerable<UserModel>> KullaniciListesiGetirAsync();
        Task<UserModel> KaydetKullaniciAsync(UserModel kullaniciModel);
        Task<int> SilKullaniciAsync(int id);
        Task<UserModel> KullaniciGetirAsync(int id);
    }
}
