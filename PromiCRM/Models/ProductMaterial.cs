using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class ProductMaterial
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(MaterialWarehouse))]
        public int MaterialWarehouseId { get; set; }
        public virtual MaterialWarehouse MaterialWarehouse { get; set; }

        
    }
}
