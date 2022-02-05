using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateCountryDTO
    {
       
        public string Name { get; set; }
       
        public string ShortName { get; set; }
  
    }

    public class UpdateCountryDTO
    {

    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
    }
}
