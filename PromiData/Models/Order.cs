using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromiData.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CurrencyName { get; set; }
        // we need to conect the user table some how with the order tab
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey(nameof(Order))]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string OrderType { get; set; }
        //for image
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        /*[NotMapped]
        public IFormFile File { get; set; }*/
        public bool Status { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Platforma { get; set; }
        // start. we can take from warehouse product if it exist
        public int WarehouseProductsNumber { get; set; }
        public DateTime? WarehouseProductsDate { get; set; }
        public bool WarehouseProductsTaken { get; set; }
        //end
        public string MoreInfo { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
        [ForeignKey(nameof(Shipment))]
        public int? ShipmentTypeId { get; set; }
        public virtual Shipment Shipment { get; set; }
        [ForeignKey(nameof(Customer))]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string Device { get; set; }
        public int ProductionTime { get; set; }
        public string Address { get; set; }
        [ForeignKey(nameof(Country))]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Comment { get; set; }
        public double? Price { get; set; }
        public double? Vat { get; set; }
        public DateTime OrderFinishDate { get; set; }

        public virtual ICollection<WarehouseCounting> WarehouseCountings { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }

    }
}
