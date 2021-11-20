using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class LoginUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDTO : LoginUserDTO
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TypeId { get; set; }
        public UserType Type { get; set; }
        public virtual IList<BonusDTO> Bonus { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
        public virtual IList<WeeklyWorkScheduleDTO> WeeklyWorkSchedules { get; set; }
    }
}
