using BakTraCam.Core.DataAccess.Context;
using BakTraCam.Core.DataAccess.Repositories.Base;
using BakTraCam.Core.Entity;

namespace BakTraCam.Core.DataAccess.Repositories.User
{
    public sealed class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(BakTraCamContext context): base(context) { }
    }
}
