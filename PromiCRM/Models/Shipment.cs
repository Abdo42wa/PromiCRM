using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }
        public double ShippingCost { get; set; }
        public int ShippingNumber { get; set; }
        public string ShipmentInfo { get; set; }
    }
}
