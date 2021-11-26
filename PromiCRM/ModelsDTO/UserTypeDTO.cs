using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.ModelsDTO
{
    public class CreateUserTypeDTO
    {
        public string Title { get; set; }
    }
    public class UserTypeDTO
    {
        public int Id { get; set; }
        public virtual IList<UserDTO> Users { get; set; }
    }
}
