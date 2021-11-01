using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Order
    {
        public int Id { get; set; }

        // we need to conect the user table some how with the order tab
        [ForeignKey(nameof(ApiUser))]
        public ApiUser ApiUser { get; set; }
        public int UserId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Data { get; set; }
        public string Platformas { get; set; }
        public string MoreInfo { get; set; }
        public int quantity { get; set; }
        public string Photo { get; set; }
        public string productCode { get; set; }

        [ForeignKey(nameof(shipmentType))]
        public ShipmentType shipmentType { get; set; }
        public int ShipmentTypeId { get; set; }

        [ForeignKey(nameof(Customer))]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public string Address { get; set; }

        [ForeignKey(nameof(Country))]
        public Country Country { get; set; }
        public int CountryId { get; set; }

        public string Comment { get; set; }

        public double Price { get; set; }

        [ForeignKey(nameof(Currency))]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }

        public double Vat { get; set; }

        public DateTime OrderFinishDate { get; set; }




    }
}
