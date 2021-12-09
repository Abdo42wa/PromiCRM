using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class MaterialWarehouseForm
    {
        public string Title { get; set; }
        public string MeasuringUnit { get; set; }
        public int Quantity { get; set; }
        public string Info { get; set; }
        public int DeliveryTime { get; set; }
        public int UseDays { get; set; }
        public DateTime LastAdittion { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
    }
}
