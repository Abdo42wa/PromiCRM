using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get;set; }
        public virtual IList<Bonus> Bonus { get; set; }
        public virtual IList<Order> Orders { get; set; }
        public virtual IList<WeeklyWorkSchedule> WeeklyWorkSchedules { get; set; }
    }
}
