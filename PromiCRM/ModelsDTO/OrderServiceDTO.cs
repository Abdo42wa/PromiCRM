using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateOrderServiceDTO
    {
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        //and service id(laseriavimas,frezavimas ...)
        public int ServiceId { get; set; }
        public int TimeConsumption { get; set; }
        public Guid? UserId { get; set; }
    }
    public class UpdateOrderServiceDTO : CreateOrderServiceDTO
    {

    }
    public class OrderServiceDTO
    {
        public int Id { get; set; }
        public virtual ProductDTO Product { get; set; }
        public virtual OrderDTO Order { get; set; }
        public virtual ServiceDTO Service { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
