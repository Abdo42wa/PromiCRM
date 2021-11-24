using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
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
