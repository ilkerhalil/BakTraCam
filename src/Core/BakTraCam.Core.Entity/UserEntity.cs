using BakTraCam.Core.Entity.Base;

namespace BakTraCam.Core.Entity
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public int UnvanId { get; set; }

        // 1 sorumlu
        // 2 bakımcı
    }
}
