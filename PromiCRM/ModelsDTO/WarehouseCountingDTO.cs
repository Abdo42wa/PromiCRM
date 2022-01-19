using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
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
