﻿using PromiCRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserDTO : LoginUserDTO
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserType Type { get; set; }
        public byte[] UserPhoto { get; set; }
        public virtual IList<BonusDTO> Bonus { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
        public virtual IList<WeeklyWorkScheduleDTO> WeeklyWorkSchedules { get; set; }
    }
}
