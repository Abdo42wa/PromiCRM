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
        public int Quantity { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required] 
        public string ProductCode { get; set; }
        [Required]
        public int ShipmentTypeId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 10, ErrorMessage = "The Order  Address cannot be more than 150 or less than 10 ")]
        public string Address { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string Comment { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        public double Vat { get; set; }
        [Required]
        public DateTime OrderFinishDate { get; set; }
    }

    public class UpdateOrderDTO : CreateOrderDTO
    {

    }

    public class OrderDTO : CreateOrderDTO
    {
        public int Id { get; set; }
        public UserDTO ApiUser { get; set; }
        public CustomerDTO Customer { get; set; }
        public ShipmentDTO Shipment { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }

        public virtual IList<WarehouseCountingDTO> WarehouseCountings { get; set; }
        public virtual IList<ProductDTO> Products { get; set; }
    }
}
