using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        //relationship one to many. Each Customer can have  multiple orders.
        public virtual IList<Order> Orders { get; set; }
        public virtual IList<NonStandardWorks> NonStandardWorks { get; set; }
    }
}
