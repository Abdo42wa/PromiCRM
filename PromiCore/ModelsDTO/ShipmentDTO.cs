using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiCore.ModelsDTO
{
    public class CreateShipmentDTO
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public int Period { get; set; }
        [Required]
        public double ShippingCost { get; set; }
        [Required]
        public int ShippingNumber { get; set; }
        public string ShipmentInfo { get; set; }
    }
    public class UpdateShipmentDTO : CreateShipmentDTO
    {

    }
    public class ShipmentDTO : CreateShipmentDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
    }
}
