using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PromiCRM.Services
{

    public class AuthManager : IAuthManager
    {
        public Task<string> CreateToken()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
