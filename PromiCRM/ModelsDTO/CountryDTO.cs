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
<<<<<<< Updated upstream:PromiCRM/ModelsDTO/CountryDTO.cs
  
=======
        public string Continent { get; set; }

>>>>>>> Stashed changes:PromiCore/ModelsDTO/CountryDTO.cs
    }

    public class UpdateCountryDTO : CreateCountryDTO
    {

    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
    }
}
