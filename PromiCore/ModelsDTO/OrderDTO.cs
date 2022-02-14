using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace PromiCore.ModelsDTO
{
    public class CreateOrderDTO
    {
        public Guid UserId { get; set; }
        public string CountryName { get; set; }
        public string CurrencyName { get; set; }
        public string OrderType { get; set; }
        //for image
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
        public int? ProductId { get; set; }
        public bool Status { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime OrderFinishDate { get; set; }
        public string Platforma { get; set; }
        // start. we can take from warehouse product if it exist
        public int WarehouseProductsNumber { get; set; }
        public DateTime? WarehouseProductsDate { get; set; }
        public bool WarehouseProductsTaken { get; set; }
        //end
        public string MoreInfo { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
        public int? ShipmentTypeId { get; set; }
        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string ShippingCost { get; set; }
        public string ShippingNumber { get; set; }
        public string Device { get; set; }
        public int ProductionTime { get; set; }
        public string Address { get; set; }
        public int? CountryId { get; set; }
        public string Comment { get; set; }
        public double? Price { get; set; }
        public double? Vat { get; set; }
        public IList<OrderServiceDTO> OrderServices { get; set; }
    }

    public class UpdateOrderDTO : CreateOrderDTO
    {
        public IList<UserServiceDTO> UserServices { get; set; }
    }
    public class OrderDTO : CreateOrderDTO
    {
        public int Id { get; set; }
        public DateTime MinOrderFinishDate { get; set; }
        public int WeekNumber { get; set; }
        //done time
     

        public UserDTO User { get; set; }
        public ProductDTO Product { get; set; }
        public CustomerDTO Customer { get; set; }
        public ShipmentDTO Shipment { get; set; }
        public CountryDTO Country { get; set; }

        public IList<WarehouseCountingDTO> WarehouseCountings { get; set; }
        public IList<ProductMaterialDTO> ProductMaterials { get; set; }
        public IList<UserServiceDTO> UserServices { get; set; }

    }

}
