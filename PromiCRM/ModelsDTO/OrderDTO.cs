using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateOrderDTO
    {
        [Required]
        
        public int OrderNumber { get; set; }
        [Required]
       
        public DateTime Data { get; set; }
        [Required]
      
        public string Platformas { get; set; }
        public string MoreInfo { get; set; }
        [Required]
       
        public int quantity { get; set; }
        [Required]
  
        public string Photo { get; set; }
        [Required]
        
        public string productCode { get; set; }

        /* [ForeignKey(nameof(shipmentType))]
         public Shipment shipmentType { get; set; }
         public int ShipmentTypeId { get; set; }*/

        /*[ForeignKey(nameof(Customer))]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }*/
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 10, ErrorMessage = "The Order  Address cannot be more than 150 or less than 10 ")]
        public string Address { get; set; }

        /*[ForeignKey(nameof(Country))]
        public Country Country { get; set; }
        public int CountryId { get; set; }*/

        public string Comment { get; set; }
        [Required]
        public double Price { get; set; }

        /*[ForeignKey(nameof(Currency))]
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }*/

        public double Vat { get; set; }
        [Required]
        public DateTime OrderFinishDate { get; set; }
    }

    public class UpdateOrderDTO : CreateOrderDTO
    {

    }

    public class OrderDTO
    {
        public int Id { get; set; }
        public virtual IList<UserDTO> Users { get; set; }
        public virtual IList<CustomerDTO> Customer { get; set; }
        public virtual IList<ShipmentDTO> Shipment { get; set; }
        public virtual IList<CountryDTO> Country { get; set; }
        public virtual IList<CurrencyDTO> Currency { get; set; }
    }
}
