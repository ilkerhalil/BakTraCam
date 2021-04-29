using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Core.Business.Domain.User;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.User
{
    public class UserApplication : ApplicationBase<UserApplication>, IUserApplication
    {
        private readonly IUserDomain _kullaniciDom;
 
        //}
        public UserApplication(IDatabaseUnitOfWork uow, IUserDomain kullaniciDom)
            : base(uow)
        {
            _kullaniciDom = kullaniciDom;
        }


        public Task<IEnumerable<UserModel>> KullaniciListesiGetirAsync()
        {
            return _kullaniciDom.KullanicilariGetirAsync();
        }
        public Task<UserModel> KaydetKullaniciAsync(UserModel kullaniciModel)
        {
            return _kullaniciDom.KaydetKullaniciAsync(kullaniciModel);
        }

        public Task<int> SilKullaniciAsync(int id)
        {
            return _kullaniciDom.SilKullaniciAsync(id);
        }

        public Task<UserModel> KullaniciGetirAsync(int id)
        {
            return _kullaniciDom.KullaniciGetirAsync(id);
        }


    }
}
