using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromiCRM.IRepository;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using PromiCRM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        private readonly IAuthManager _authManager;

        public AccountsController(IUnitOfWork unitOfWork,IMapper mapper, ILogger<AccountsController> logger, IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _authManager = authManager;
        }

         /// <summary>
        /// get all users convert to dtos and return
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAll();
            var results = _mapper.Map<IList<UserDTO>>(users);

            return Ok(results);
        }

        /// <summary>
        /// In Register we'll requiring sensitive data like passwords we dont want to send them as parameters
        /// We use POST request. when we do get request its sent accross in plain text. Get info from body
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            /*
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            //check if it's valid state. if you didnt include email or smth,
            //according to our standarts that we set in dto
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map/convert userDTO object to ApiUser domain object(for database)
            var user = _mapper.Map<ApiUser>(userDTO);
            //ApiUser has Username not email
            user.UserName = userDTO.Email;
            //_userManager createAsync method will automatically hash password, store everything. 
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            //check if registration succeded
            if (!result.Succeeded)
            {
                //for each error addModelError with status code and description
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            //if user was added we can add list of Roles to him
            await _userManager.AddToRolesAsync(user, userDTO.Roles);

            //return anything in 200 range. means it was succesful
            return Accepted();
            */
            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginUser([FromBody] UserDTO userDTO)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ValidateUser returns true or false
            var validUser = await _authManager.ValidateUser(userDTO);
            if (validUser == false)
            {
                return Unauthorized();
            }

            //return anything in 200 range. means it was succesful
            // return new object iwth an expression called Token. It'lll equal to
            // authManager method CrateToken which will return Token

            return Accepted(new { Token = await _authManager.CreateToken() });
            */
            return Ok();
        }

    }
}
