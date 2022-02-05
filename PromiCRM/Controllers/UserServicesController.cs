using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromiCRM.IRepository;
using PromiCRM.Models;
using PromiCRM.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromiCRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserServicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserServicesController> _logger;

        public UserServicesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserServicesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserServices()
        {
            var userServices = await _unitOfWork.UserServices.GetAll();
            var results = _mapper.Map<IList<UserServiceDTO>>(userServices);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetUserServiceById")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserService(int id)
        {
            var userService = await _unitOfWork.UserServices.Get(u => u.Id == id);
            var results = _mapper.Map<UserServiceDTO>(userService);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserService([FromBody]CreateUserServiceDTO userServiceDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateUserService)}");
                return BadRequest("Submited invalid data");
            }
            var userService = _mapper.Map<UserService>(userServiceDTO);
            await _unitOfWork.UserServices.Insert(userService);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetUserServiceById", new { id = userService.Id }, userService);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserService([FromBody]UpdateUserServiceDTO userServiceDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUserService)}");
                return BadRequest("Submited invalid data");
            }
            var userService = await _unitOfWork.UserServices.Get(u => u.Id == id);
            if(userService == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUserService)}");
                return BadRequest("Submited invalid data");
            }
            //put all values from dto to model
            _mapper.Map(userServiceDTO, userService);
            _unitOfWork.UserServices.Update(userService);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUserService(int id)
        {
            var userService = await _unitOfWork.UserServices.Get(s => s.Id == id);
            if(userService == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteUserService)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.UserServices.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
