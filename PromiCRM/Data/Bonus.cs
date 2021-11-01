using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Data
{
    public class Bonus
    {
        public int id { get; set; }
        [ForeignKey(nameof(ApiUser))]
        public ApiUser ApiUser { get; set; }
        public int UserId { get; set; }
        public int Kiekis { get; set; }
        public int Sukaupta { get; set; }
        public int Bounuas { get; set; }
        public int LikoIKI { get; set; }
    }
}
