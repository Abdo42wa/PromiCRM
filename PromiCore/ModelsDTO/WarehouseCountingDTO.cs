using System;

namespace PromiCore.ModelsDTO
{

    public class CreateWarehouseCountingDTO
    {
        public int QuantityProductWarehouse { get; set; }
        /*    public string ImageName { get; set; }
            public string ImagePath { get; set; }
            public IFormFile File { get; set; }*/
        public string ProductCode { get; set; }
        public DateTime LastTimeChanging { get; set; }
        public int OrderId { get; set; }
    }

    public class UpdateWarehouseCountingDTO : CreateWarehouseCountingDTO
    {

    }

    public class WarehouseCountingDTO : CreateWarehouseCountingDTO
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public OrderDTO Order { get; set; }
    }
}
