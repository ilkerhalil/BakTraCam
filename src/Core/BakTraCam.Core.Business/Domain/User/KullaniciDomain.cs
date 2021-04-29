using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakTraCam.Core.DataAccess.Repositories.User;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Core.Entity;
using BakTraCam.Service.DataContract;
using BakTraCam.Util.Mapping.Adapter;

namespace BakTraCam.Core.Business.Domain.User
{
    public  class UserDomain:DomainBase<UserDomain>,IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(ICustomMapper mapper, IDatabaseUnitOfWork uow,IUserRepository userRepository) 
            : base(mapper, uow)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserModel>> KullanicilariGetirAsync()
        {
            return (await _userRepository.ListAsync<UserModel>()).OrderBy(m => m.Name);
        }
        public async Task<UserModel> KaydetKullaniciAsync(UserModel model)
        {
            var kullanici = model.Id > 0 ? await _userRepository.FirstOrDefaultAsync(m => m.Id == model.Id) : null;
            if (null == kullanici)
            {
                kullanici = Mapper.Map<UserModel, UserEntity>(model);

                await _userRepository.AddAsync(kullanici);
                await Uow.SaveChangesAsync();
            }
            else
            {
                Mapper.Map(model, kullanici);

                await _userRepository.UpdateAsync(kullanici);
                await Uow.SaveChangesAsync();
            }
             
            return Mapper.Map<UserEntity, UserModel>(kullanici);

        }

        public async Task<int> SilKullaniciAsync(int id)
        {
            await _userRepository.DeleteAsync(a => a.Id == id);
            await Uow.SaveChangesAsync();
            return id;
        }

        public async Task<UserModel> KullaniciGetirAsync(int id)
        {
            var result = await _userRepository.FirstOrDefaultAsync<UserModel>(a => a.Id == id);
            return result;
        }

        
    }
}
