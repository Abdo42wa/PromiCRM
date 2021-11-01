using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Data
{
    public class WarehouseCounting
    {
        public int Id { get; set; }
        public int QuantityProductWarehouse { get; set; }
        public string Photo { get; set; }
        public DateTime LastTimeChanging  { get; set; }

        [ForeignKey(nameof(Order))]
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
