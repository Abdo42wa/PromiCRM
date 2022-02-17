using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiData.Models
{
    public class Bonus
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        //bascially particular month that bonus will apply to
        public DateTime Date { get; set; }
        //how many bonuses have in this month. maybe goal was 1000 but team made 2000. then accumulated will be 2
        public int Accumulated { get; set; }
        public virtual ICollection<UserBonus> UserBonuses { get; set; }
    }
}
