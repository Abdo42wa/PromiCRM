using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PromiData.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Continent { get; set; }

        //relationship one to many. each country can have multiple orders
        public ICollection<Order> Orders { get; set; }
    }
}
