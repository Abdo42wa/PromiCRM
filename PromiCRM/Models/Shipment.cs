using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }
        public double ShippingCost { get; set; }
        public int ShippingNumber { get; set; }
        public string ShipmentInfo { get; set; }
        //relationship one to many. each shipment can have multiple orders
        public virtual ICollection<Order> Orders { get; set; }
    }
}
