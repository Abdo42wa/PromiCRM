﻿using System;
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
    }
    public class UpdateOrderServiceDTO : CreateOrderServiceDTO
    {

    }
    public class OrderServiceDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public OrderDTO Order { get; set; }
        public ServiceDTO Service { get; set; }
        public IList<UserServiceDTO> UserServices { get; set; }
    }
}