using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromiData.Models
{
    public class UserService
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(OrderService))]
        public int OrderServiceId { get; set; }
        public virtual OrderService OrderService { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        ///OrderId i need for Standart and Warehouse orders. becouse we need to asign completed
        ///tasks for orders that we make. And when we create Product we create many OrderService obj with TimeConsumption
        ///it has productId but doesnt have orderId. becouse we asign TimeConsumption of services(frezavimas) for particular Product
        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        public DateTime CompletionDate { get; set; }

        /*[ForeignKey(nameof(ProductService))]
        public int? ProductServiceId { get; set; }

        public virtual ProductService ProductService { get; set; }*/
    }
}
