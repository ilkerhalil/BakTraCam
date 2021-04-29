using BakTraCam.Core.DataAccess.Context;
using BakTraCam.Core.DataAccess.Repositories.Base;
using BakTraCam.Core.Entity;

namespace BakTraCam.Core.DataAccess.Repositories.Notice
{
    public sealed class NoticeRepository : BaseRepository<NoticeEntity>, INoticeRepository
    {
        public NoticeRepository(BakTraCamContext context): base(context) { }
    }
}
