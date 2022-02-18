using System;

namespace PromiCore.ModelsDTO
{
    public class CreateUserServiceDTO
    {
        public int OrderServiceId { get; set; }
        public Guid UserId { get; set; }
        public int? OrderId { get; set; }
        public DateTime CompletionDate { get; set; }
        public int? ProductServiceId { get; set; }
    }
    public class UpdateUserServiceDTO : CreateUserServiceDTO
    {
    }
    public class UserServiceDTO : CreateUserServiceDTO
    {
        public int Id { get; set; }
        public OrderServiceDTO OrderService { get; set; }
        public UserDTO User { get; set; }
        public OrderDTO Order { get; set; }

        /*public  ProductServiceDTO ProductService { get; set; }*/
    }

    public class UserMadeServicesDTO : CreateUserServiceDTO
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
        public OrderServiceDTO OrderService { get; set; }
        public UserDTO User { get; set; }
        public OrderDTO Order { get; set; }

        /*public  ProductServiceDTO ProductService { get; set; }*/
    }

}
