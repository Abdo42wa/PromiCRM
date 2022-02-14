using PromiCore.ModelsDTO;
using System.Threading.Tasks;

namespace PromiCore.Services
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
