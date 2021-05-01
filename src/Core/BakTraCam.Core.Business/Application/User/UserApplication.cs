using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Core.Business.Domain.User;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.User
{
    public class UserApplication : ApplicationBase<UserApplication>, IUserApplication
    {
        private readonly IUserDomain _userDomain;
 
        public UserApplication(IDatabaseUnitOfWork uow, IUserDomain userDomain)
            : base(uow)
        {
            _userDomain = userDomain;
        }


        public Task<IEnumerable<UserModel>> KullaniciListesiGetirAsync()
        {
            return _userDomain.KullanicilariGetirAsync();
        }
        public Task<UserModel> KaydetKullaniciAsync(UserModel kullaniciModel)
        {
            return _userDomain.KaydetKullaniciAsync(kullaniciModel);
        }

        public Task<int> SilKullaniciAsync(int id)
        {
            return _userDomain.SilKullaniciAsync(id);
        }

        public Task<UserModel> KullaniciGetirAsync(int id)
        {
            return _userDomain.KullaniciGetirAsync(id);
        }


    }
}
