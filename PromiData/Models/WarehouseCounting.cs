using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromiData.Models
{
    public class WarehouseCounting
    {

        [Key]
        public int Id { get; set; }
        public int QuantityProductWarehouse { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        /*        [NotMapped]
                public IFormFile File { get; set; }*/
        public DateTime LastTimeChanging { get; set; }
        public string ProductCode { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}
