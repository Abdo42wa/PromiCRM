using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{

    public class CreateWarehouseCountingDTO
    {
        [Required]
        public int QuantityProductWarehouse { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public DateTime LastTimeChanging { get; set; }

       /* [ForeignKey(nameof(Order))]
        public Order Order { get; set; }
        public int OrderId { get; set; }*/
    }

    public class UpdateWarehouseCountingDTO : CreateWarehouseCountingDTO
    {

    }

    public class WarehouseCountingDTO : CreateWarehouseCountingDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Order { get; set; }
    }
}
