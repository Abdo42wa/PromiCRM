using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PromiCore.IRepository;
using PromiCore.ModelsDTO;
using PromiData.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PromiCore.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthManager(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        /// <summary>
        /// These are things that are needed for JWT token creation.
        /// Create object for singingCredentials, create claims and tokenOptions
        /// return JWT token with help of JwtSecurityTokenHandler method writeToken. providing tokenOptions
        /// token will combine signing credentials and claims into one and create actual token
        /// at last we serialize it into string and return that string
        /// </summary>
        /// <returns></returns>
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Getting jwtSettings from appsettings. then token equal to new JwtSecurityToken
        /// setting issuer, then claims thats passed to this function. then set expiration. get that from appsettings.json
        /// it will be available for 15 minutes. then set signingCredentials
        /// </summary>
        /// <param name="signingCredentials"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            /*            var lifetime = Environment.GetEnvironmentVariable("lifetime");
                        var issuer = Environment.GetEnvironmentVariable("Issuer");*/
            var jwtSettings = _configuration.GetSection("Jwt");

            /*var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(lifetime));*/
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            return token;
        }

        /// <summary>
        /// It will return List of Claims. setting new list of claims
        /// claims tell who are you and what you can do. adding Claim of claimType name(username or name or email)
        /// name will come from _user object. Then getting list of roles with _userManager method. just provide _user object
        /// for each role we want to ADD new Claim of claimType role
        /// </summary>
        /// <returns></returns>
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.Email),
                new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString())
            };
            var role = await _unitOfWork.UserTypes.Get(u => u.Id == _user.TypeId);
            var roleName = role.Title;
            claims.Add(new Claim(ClaimTypes.Role, roleName));
            /*var role = await _unitOfWork
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            */
            return claims;
        }

        /// <summary>
        /// For this method we need to get KEY
        /// getting key that i set with Command line. Encoding key to get secret
        /// returning new SigningCredentials it'll have secret, and let it know that security algo used
        /// for this was HmacSha256
        /// </summary>
        /// <returns></returns>
        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        /// <summary>
        /// Check if user exists in System and if password is correct
        /// if user was found and if password was correct return TRUE otherwise FALSE
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            _user = await _unitOfWork.Users.Get(u => u.Email == userDTO.Email);
            if (_user != null && BCrypt.Net.BCrypt.Verify(userDTO.Password, _user.Password) == false)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}