﻿using BakTraCam.Core.DataAccess.Repositores;
using BakTraCam.Service.DataContract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakTraCam.Core.DataAccess.Repositores.Kullanici;
using BakTraCam.Core.Entity;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Util.Mapping.Adapter;

namespace BakTraCam.Core.Business.Domain.Kullanici
{
    public  class KullaniciDomain:DomainBase<KullaniciDomain>,IKullaniciDomain
    {
        private readonly IDatabaseUnitOfWork _uow;
        private readonly IKullaniciRepository _kullaniciRep;
        //public KullaniciDomain(IServiceProvider serviceProvider) : base(serviceProvider)
        //{
        //    _kullaniciRep = serviceProvider.GetService<IKullaniciRepository>();
        //    _uow = serviceProvider.GetService<IDatabaseUnitOfWork>();
        //}

        public KullaniciDomain(ICustomMapper mapper, IDatabaseUnitOfWork uow,IKullaniciRepository kullaniciRep) 
            : base(mapper, uow)
        {
            _kullaniciRep = kullaniciRep;
        }

        public async Task<IEnumerable<KullaniciModel>> KullanicilariGetirAsync()
        {
            return (await _kullaniciRep.ListAsync<KullaniciModel>()).OrderBy(m => m.Ad);
        }
        public async Task<KullaniciModel> KaydetKullaniciAsync(KullaniciModel model)
        {
            var kullanici = model.Id > 0 ? await _kullaniciRep.FirstOrDefaultAsync(m => m.Id == model.Id) : null;
            if (null == kullanici)
            {
                kullanici = Mapper.Map<KullaniciModel, KullaniciEntity>(model);

                await _kullaniciRep.AddAsync(kullanici);
                await _uow.SaveChangesAsync();
            }
            else
            {
                Mapper.Map(model, kullanici);

                await _kullaniciRep.UpdateAsync(kullanici);
                await _uow.SaveChangesAsync();
            }
             
            return Mapper.Map<KullaniciEntity, KullaniciModel>(kullanici);

        }

        public async Task<int> SilKullaniciAsync(int id)
        {
            await _kullaniciRep.DeleteAsync(a => a.Id == id);
            await _uow.SaveChangesAsync();
            return id;
        }

        public async Task<KullaniciModel> KullaniciGetirAsync(int id)
        {
            var result = await _kullaniciRep.FirstOrDefaultAsync<KullaniciModel>(a => a.Id == id);
            return result;
        }

        
    }
}
