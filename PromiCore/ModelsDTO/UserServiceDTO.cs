using System;

namespace PromiCore.ModelsDTO
{
    public class CreateUserServiceDTO
    {
        public int OrderServiceId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CompletionDate { get; set; }
    }
    public class UpdateUserServiceDTO : CreateServiceDTO
    {
    }
    public class UserServiceDTO : CreateUserServiceDTO
    {
        public int Id { get; set; }
        public OrderServiceDTO OrderService { get; set; }
        public UserDTO User { get; set; }
    }

}
