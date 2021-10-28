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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProductDTO>(product));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }        
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ProductDTO model)
        {
            try
            {
               var addedProduct = await _productService.AddAsync(_mapper.Map<Product>(model));
               return Created(string.Empty, _mapper.Map<ProductDTO>(addedProduct));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(ProductDTO model)
        {
            try
            {
               var updatedProduct = _productService.Update(_mapper.Map<Product>(model));
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
                var product = _productService.GetByIdAsync(id).Result;
                if (product == null)
                {
                    return NotFound();
                }
                _productService.Remove(product);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
