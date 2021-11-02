using PromiCRM.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Services
{
    /// <summary>
    /// Defining all methods we will use for Authentication in AuthManager class.
    /// Validate user will return bool and createTOken will return string(Token)
    /// </summary>
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
