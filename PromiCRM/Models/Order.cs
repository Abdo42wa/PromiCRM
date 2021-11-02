using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        // we need to conect the user table some how with the order tab
        [ForeignKey(nameof(ApiUser))]
        public Guid UserId { get; set; }
        [NotMapped]
        public ApiUser ApiUser { get; set; }
        
        public int OrderNumber { get; set; }
        public DateTime Data { get; set; }
        public string Platformas { get; set; }
        public string MoreInfo { get; set; }
        public int Quantity { get; set; }
        public string Photo { get; set; }
        public string ProductCode { get; set; }

        [ForeignKey(nameof(Shipment))]
        public int ShipmentTypeId { get; set; }
        [NotMapped]
        public Shipment Shipment { get; set; }
        

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [NotMapped]
        public Customer Customer { get; set; }
        
        public string Address { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        [NotMapped]
        public Country Country { get; set; }
        

        public string Comment { get; set; }

        public double Price { get; set; }

        [ForeignKey(nameof(Currency))]
        public int CurrencyId { get; set; }
        [NotMapped]
        public Currency Currency { get; set; }
        

        public double Vat { get; set; }

        public DateTime OrderFinishDate { get; set; }


        public virtual IList<WarehouseCounting> WarehouseCountings { get; set; }
        public virtual IList<Product> Products { get; set; }

    }
}
