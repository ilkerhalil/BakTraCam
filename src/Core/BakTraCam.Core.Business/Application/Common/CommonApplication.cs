using System.Collections.Generic;
using System.Threading.Tasks;
using BakTraCam.Core.Business.Domain.Common;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;

namespace BakTraCam.Core.Business.Application.Common
{
    public class CommonApplication : ApplicationBase<CommonApplication>, ICommonApplication
    {
        private readonly ICommonDomain _commonDomain;
        //private readonly IDatabaseUnitOfWork _uow ;
        //public OrtakApplication(IServiceProvider serviceProvider) : base(serviceProvider)
        //{
        //    _ortakDom = serviceProvider.GetService<IOrtakDomain>();
        //    _uow = serviceProvider.GetService<IDatabaseUnitOfWork>();
        //}
        public CommonApplication(IDatabaseUnitOfWork uow, ICommonDomain commonDomain)
            : base(uow)
        {
            _commonDomain = commonDomain;
        }


        public Task<IEnumerable<SelectModel>> KullaniciListesiGetirAsync()
        {
            return _commonDomain.KullanicilariGetirAsync();
        }

        public Task<IEnumerable<NoticeModel>> DuyuruListesiGetirAsync()
        {
            return _commonDomain.DuyurulariGetirAsync();
        }
        public Task<NoticeModel> KaydetDuyuruAsync(NoticeModel noticeModel)
        {
            return _commonDomain.KaydetDuyuruAsync(noticeModel);
        }

        public Task<int> SilDuyuruAsync(int id)
        {
            return _commonDomain.SilDuyuruAsync(id);
        }

        public Task<NoticeModel> DuyuruGetirAsync(int id)
        {
            return _commonDomain.DuyuruGetirAsync(id);
        }


    }
}
