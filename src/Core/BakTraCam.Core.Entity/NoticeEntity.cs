using System;
using BakTraCam.Core.Entity.Base;

namespace BakTraCam.Core.Entity
{
    public class NoticeEntity:BaseEntity
    {
        public string Aciklama { get; set; }
        public DateTime Tarihi { get; set; }
    }
}
