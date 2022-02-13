using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiCore.ModelsDTO
{
   public class WorkTimeDTO
    {
        public int BondingTime { get; set; }
        public int CollectionTime { get; set; }
        public int LaserTime { get; set; }
        public int MilingTime { get; set; }
        public int PaintingTime { get; set; }
        public int PackingTime { get; set; }
        public int GrindingTime { get; set; }

        public bool Status { get; set; }

        public int Quantity { get; set; }



        public int DoneBondingTime { get; set; }
        public int DoneCollectionTime { get; set; }
        public int DoneLaserTime { get; set; }
        public int DoneMilingTime { get; set; }
        public int DonePaintingTime { get; set; }
        public int DonePackingTime { get; set; }
        public int DoneGrindingTime { get; set; }
        public int? ProductId { get; set; }
        public ProductDTO Product { get; set; }

    }
}
