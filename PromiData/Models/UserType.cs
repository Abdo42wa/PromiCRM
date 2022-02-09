using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiData.Models
{
    // userType can have many users
    public class UserType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
