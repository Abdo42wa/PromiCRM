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
        public int? ProductId { get; set; }
        public bool Status { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
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
        public string Device { get; set; }
        public int ProductionTime { get; set; }
        public string Address { get; set; }
        public int? CountryId { get; set; }
        public string Comment { get; set; }
        public double? Price { get; set; }
        public int? CurrencyId { get; set; }
        public double? Vat { get; set; }
        public DateTime OrderFinishDate { get; set; }

        public string BondingUserId { get; set; }
        public string CollectionUserId { get; set; }
        public string LaserUserId { get; set; }
        public string MilingUserId { get; set; }
        public string PaintingUserId { get; set; }
        public string PackingUserId { get; set; }

        public int? BondingTime { get; set; }
        public int? CollectionTime { get; set; }
        public int? LaserTime { get; set; }
        public int? MilingTime { get; set; }
        public int? PaintingTime { get; set; }
        public int? PackingTime { get; set; }

        public DateTime? BondingComplete { get; set; }
        public DateTime? CollectionComplete { get; set; }
        public DateTime? LaserComplete { get; set; }
        public DateTime? MilingComplete { get; set; }
        public DateTime? PaintingComplete { get; set; }
        public DateTime? PackingComplete { get; set; }


    }

    public class UpdateOrderDTO : CreateOrderDTO
    {

    }
    public class OrderDTO : CreateOrderDTO
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime MinOrderFinishDate { get; set; }
        public int WeekNumber { get; set; }

        //done time
        public int DoneBondingTime { get; set; }
        public int DoneCollectionTime { get; set; }
        public int DoneLaserTime { get; set; }
        public int DoneMilingTime { get; set; }
        public int DonePaintingTime { get; set; }
        public int DonePackingTime { get; set; }

        public UserDTO User { get; set; }
        public CustomerDTO Customer { get; set; }
        public ShipmentDTO Shipment { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }

        public virtual IList<WarehouseCountingDTO> WarehouseCountings { get; set; }
        public ProductDTO Product { get; set; }
    }

}
