using System;
using BakTraCam.Core.Entity.Base;

namespace BakTraCam.Core.Entity
{
    public class MaintenanceEntity : BaseEntity
    {

        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Gerceklestiren1 { get; set; }
        public int Gerceklestiren2 { get; set; }
        public int Gerceklestiren3 { get; set; }
        public int Gerceklestiren4 { get; set; }
        public int Sorumlu1 { get; set; }
        public int Sorumlu2 { get; set; }
        public int Period { get; set; }
        public int Durum { get; set; }
        public int Tip { get; set; }
    }
}
