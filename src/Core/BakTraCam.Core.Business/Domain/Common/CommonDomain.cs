using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakTraCam.Core.Business.Domain.User;
using BakTraCam.Core.DataAccess.Repositories.Notice;
using BakTraCam.Core.DataAccess.Repositories.User;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Core.Entity;
using BakTraCam.Service.DataContract;
using BakTraCam.Util.Mapping.Adapter;

namespace BakTraCam.Core.Business.Domain.Common
{
    public  class CommonDomain:DomainBase<UserDomain>,ICommonDomain
    {
        private readonly IUserRepository _kullaniciRep;
        private readonly INoticeRepository _duyuruRep;
 
        public CommonDomain(ICustomMapper mapper, IDatabaseUnitOfWork uow,IUserRepository kullaniciRep,INoticeRepository duyuruRep) : base(mapper, uow)
        {
            _kullaniciRep = kullaniciRep;
            _duyuruRep = duyuruRep;
        }

        public async Task<IEnumerable<SelectModel>> KullanicilariGetirAsync()
        {
            return (await _kullaniciRep.ListAsync<SelectModel>()).OrderBy(m => m.Name);
        }
        public async Task<IEnumerable<NoticeModel>> DuyurulariGetirAsync()
        {
            return (await _duyuruRep.ListAsync<NoticeModel>(a=>a.Tarihi>=DateTime.Now.AddMonths(-1))).OrderBy(m => m.Id);
        }
        public async Task<NoticeModel> KaydetDuyuruAsync(NoticeModel model)
        {
            var duyuru = model.Id > 0 ? await _duyuruRep.FirstOrDefaultAsync(m => m.Id == model.Id) : null;
            if (null == duyuru)
            {
                duyuru = Mapper.Map<NoticeModel, NoticeEntity>(model);
                duyuru.Tarihi = DateTime.Now;
                await _duyuruRep.AddAsync(duyuru);
                await Uow.SaveChangesAsync();
            }
            else
            {
                Mapper.Map(model, duyuru);

                await _duyuruRep.UpdateAsync(duyuru);
                await Uow.SaveChangesAsync();
            }

            return Mapper.Map<NoticeEntity, NoticeModel>(duyuru);

        }

        public async Task<int> SilDuyuruAsync(int id)
        {
            await _duyuruRep.DeleteAsync(a => a.Id == id);
            await Uow.SaveChangesAsync();
            return id;
        }

        public async Task<NoticeModel> DuyuruGetirAsync(int id)
        {
            var result = await _duyuruRep.FirstOrDefaultAsync<NoticeModel>(a => a.Id == id);
            return result;
        }

        
    }
}
