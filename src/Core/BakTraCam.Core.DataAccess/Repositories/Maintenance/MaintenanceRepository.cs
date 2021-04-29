using BakTraCam.Core.DataAccess.Context;
using BakTraCam.Core.DataAccess.Repositories.Base;
using BakTraCam.Core.Entity;

namespace BakTraCam.Core.DataAccess.Repositories.Maintenance
{
    public sealed class MaintenanceRepository : BaseRepository<MaintenanceEntity>, IMaintenanceRepository
    {
        public MaintenanceRepository(BakTraCamContext context): base(context) { }
    }
}
