using System;

namespace PromiCore.ModelsDTO
{
    public class CreateRecentWorkDTO
    {
        public Guid? UserId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public string WorkTitle { get; set; }
        public DateTime? Time { get; set; }
    }
    public class UpdateRecentWorkDTO : CreateRecentWorkDTO
    {
    }
    public class RecentWorkDTO : CreateRecentWorkDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public UserDTO User { get; set; }
    }
}
