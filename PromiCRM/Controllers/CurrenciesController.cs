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
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrenciesController> _logger;

        public CurrenciesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CurrenciesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Get all records from currencies table & convert them to dto's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrencies()
        {
            var currencies = await _unitOfWork.Currencies.GetAll();
            var results = _mapper.Map<IList<CurrencyDTO>>(currencies);
            return Ok(results);
        }
        /// <summary>
        /// Get record from currencies table by id, convert to dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetCurrency")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrency(int id)
        {
            var currency = await _unitOfWork.Currencies.Get(c => c.Id == id);
            var result = _mapper.Map<CurrencyDTO>(currency);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCurrency([FromBody]CreateCurrencyDTO currencyDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateCurrency)}");
                return BadRequest("Submited invalid data");
            }
            var currency = _mapper.Map<Currency>(currencyDTO);
            await _unitOfWork.Currencies.Insert(currency);
            await _unitOfWork.Save();
            //calling GetCurrency & providing id for it from currency, then object. It'll return currency thats created
            return CreatedAtRoute("GetCurrency", new { id = currency.Id }, currency);
        }
        /// <summary>
        /// Check if valid, check if exist & update it
        /// </summary>
        /// <param name="currencyDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCurrency([FromBody]UpdateCurrencyDTO currencyDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCurrency)}");
                return BadRequest("Submited invalid data");
            }
            var currency = await _unitOfWork.Currencies.Get(c => c.Id == id);
            if(currency == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCurrency)}");
                return BadRequest("Submited invalid data");
            }
            // convert currencyDTO to currency model. put all values to currency
            _mapper.Map(currencyDTO, currency);
            _unitOfWork.Currencies.Update(currency);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var currency = _unitOfWork.Currencies.Get(c => c.Id == id);
            if(currency == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCurrency)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.Currencies.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
