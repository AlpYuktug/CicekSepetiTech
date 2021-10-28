using AutoMapper;
using CicekSepetiTech.API.DTOs;
using CicekSepetiTech.API.Filters;
using CicekSepetiTech.Core.Models;
using CicekSepetiTech.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepetiTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baskets = await _basketService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BasketDTO>>(baskets));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var basket = await _basketService.GetByIdAsync(id);
                if (basket == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<BasketDTO>(basket));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }        
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] BasketDTO model)
        {
            try
            {
               var addedBasket = await _basketService.AddAsync(_mapper.Map<Basket>(model));
               return Created(string.Empty, _mapper.Map<BasketDTO>(addedBasket));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(BasketDTO model)
        {
            try
            {
               var updatedBasket = _basketService.Update(_mapper.Map<Basket>(model));
               return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult Remove([FromBody] int id)
        {
            try
            {
                var basket = _basketService.GetByIdAsync(id).Result;
                if (basket == null)
                {
                    return NotFound();
                }
                _basketService.Remove(basket);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
