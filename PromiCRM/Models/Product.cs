using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Product
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        

        public string Photo { get; set; }
        public string Link { get; set; }
        public string code { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public double LengthWithoutPackaging { get; set; }
        public double WidthWithoutPackaging { get; set; }
        public double HeightWithoutPackaging { get; set; }
        public double WeightGross { get; set; }
        public string PackagingBoxCode { get; set; }
        //we need to specify the type
        public double PackingTime { get; set; }
        //we need to specify the type
        /*
        public double CollectionTime { get; set; }
        //we need to specify the type
        public double GluingTime { get; set; }
        //we need to specify the type
        public double PaintingTime { get; set; }
        public double GrindingTime { get; set; }
        public double MillingTime { get; set; }
        public double laseringTime { get; set; }*/


        [ForeignKey(nameof(Services))]
        public int ServiceId { get; set; }
        public Services Services { get; set; }
        

    }
}
