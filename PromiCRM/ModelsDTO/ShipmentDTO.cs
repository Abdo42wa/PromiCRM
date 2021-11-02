using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateShipmentDTO
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "The Shipment  Type cannot be more than 50 or less than 10 ")]
        public string Type { get; set; }
        [Required]
        [Range( 20,1, ErrorMessage = "The Shipment  Period cannot be more than 20 or less than 1 ")]
        public int Period { get; set; }
        [Required]
        public double ShippingCost { get; set; }
        [Required]
        public int ShippingNumber { get; set; }
        public string ShipmentInfo { get; set; }
    }
    public class UpdateShipmentDTO: CreateShipmentDTO
    {

    }
    public class ShipmentDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
    }
}
