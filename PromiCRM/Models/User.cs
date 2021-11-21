using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey(nameof(UserType))]
        public int TypeId { get; set; }
        [NotMapped]
        public UserType Type { get; set; }
        public byte[] UserPhoto { get; set; }
        public virtual IList<Bonus> Bonus { get; set; }
        public virtual IList<Order> Orders { get; set; }
        public virtual IList<WeeklyWorkSchedule> WeeklyWorkSchedules { get; set; }
    }
}
