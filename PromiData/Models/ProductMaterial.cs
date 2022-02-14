using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromiData.Models
{
    /// <summary>
    /// OrderId can be null if its not Non-standart order.
    /// if we are making Not-standart order we do not have product.
    /// so can only save orderId
    /// </summary>
    public class ProductMaterial
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(MaterialWarehouse))]
        public int MaterialWarehouseId { get; set; }
        public virtual MaterialWarehouse MaterialWarehouse { get; set; }


    }
}
