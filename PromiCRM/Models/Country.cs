using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
<<<<<<< Updated upstream:PromiCRM/Models/Country.cs
       
=======
        public string Continent { get; set; }

        //relationship one to many. each country can have multiple orders
>>>>>>> Stashed changes:PromiData/Models/Country.cs
        public ICollection<Order> Orders { get; set; }
    }
}
