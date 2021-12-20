using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateOrderDTO
    {
        public Guid UserId { get; set; }
        public string OrderType { get; set; }
        //for image
       /* public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }*/

        public bool Status { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Data { get; set; }
        public string Platforma { get; set; }
        public string MoreInfo { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
        public int ShipmentTypeId { get; set; }
        public int CustomerId { get; set; }
        public string Device { get; set; }
        public int ProductionTime { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string Comment { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }
        public double Vat { get; set; }
        public DateTime OrderFinishDate { get; set; }

     
    }

    public class UpdateOrderDTO : CreateOrderDTO
    {

    }

    public class OrderDTO : CreateOrderDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public CustomerDTO Customer { get; set; }
        public ShipmentDTO Shipment { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }

        public virtual IList<WarehouseCountingDTO> WarehouseCountings { get; set; }
        public virtual IList<ProductDTO> Products { get; set; }
    }
}
