using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Domain.User
{
    public interface IUserDomain
    {
        Task<IEnumerable<UserModel>> KullanicilariGetirAsync();
        Task<UserModel> KaydetKullaniciAsync(UserModel model);
        Task<int> SilKullaniciAsync(int id);
        Task<UserModel> KullaniciGetirAsync(int id);
    }
}
