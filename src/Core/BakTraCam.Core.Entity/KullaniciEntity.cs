﻿using BakTraCam.Core.Entity.Base;

namespace BakTraCam.Core.Entity
{
    public class KullaniciEntity : BaseEntity
    {
        public string Ad { get; set; }
        public int UnvanId { get; set; }

        // 1 sorumlu
        // 2 bakımcı
    }
}
