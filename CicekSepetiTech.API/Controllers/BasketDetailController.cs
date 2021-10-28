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
using System.Transactions;

namespace CicekSepetiTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketDetailController : ControllerBase
    {
        private readonly IBasketDetailService _basketDetailService;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public BasketDetailController(IBasketDetailService basketDetailService, IBasketService basketService, IProductService productService, IMapper mapper)
        {
            _basketDetailService = basketDetailService;
            _basketService = basketService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var basketsDetail = await _basketDetailService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BasketDetailDTO>>(basketsDetail));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var basketDetail = await _basketDetailService.GetByIdAsync(id);
                if (basketDetail == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<BasketDetailDTO>(basketDetail));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] BasketDetailDTO model)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                //We must check basket and product. First, we'll just check the basket.
                if (await _basketService.GetByIdAsync(model.BasketId) != null)
                {
                    //Than the product number is checked.
                    var product = await _productService.GetByIdAsync(model.ProductId);
                    if (product != null)
                    {
                        //Than the quantity of the product is checked. 
                        int remainingStock = product.Stock - model.Quantity;
                        if (remainingStock >= 0)
                        {
                            var addedBasket = await _basketDetailService.AddAsync(_mapper.Map<BasketDetail>(model));
                            product.Stock = remainingStock;
                            _productService.Update(product);

                            transaction.Complete();

                            return Created(string.Empty, _mapper.Map<BasketDetailDTO>(addedBasket));
                        }
                        else
                        {
                            transaction.Dispose();
                            throw new Exception("Insufficient Stock");
                        }
                    }
                    else
                    {
                        transaction.Dispose();
                        throw new Exception("Product Id can't found");
                    }
                }
                else
                {
                    transaction.Dispose();
                    throw new Exception("Basket Id can't found");
                }
            }
            catch (Exception e)
            {
                transaction.Dispose();
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BasketDetailDTO model)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                //We must check basket and product. First, we'll just check the basket.
                if (await _basketService.GetByIdAsync(model.BasketId) != null)
                {
                    //Than the product number is checked.
                    var product = await _productService.GetByIdAsync(model.ProductId);
                    if (product != null)
                    {
                        //Than the quantity of the product is checked. 
                        int remainingStock = product.Stock - model.Quantity;
                        if (remainingStock >= 0)
                        {
                            var updatedBasket = _basketDetailService.Update(_mapper.Map<BasketDetail>(model));

                            product.Stock = remainingStock;
                            _productService.Update(product);

                            transaction.Complete();

                            return NoContent();
                        }
                        else
                        {
                            transaction.Dispose();
                            throw new Exception("Insufficient Stock");
                        }
                    }
                    else
                    {
                        transaction.Dispose();
                        throw new Exception("Product Id can't found");
                    }
                }
                else
                {
                    transaction.Dispose();
                    throw new Exception("Basket Id can't found");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var basketDetail = _basketDetailService.GetByIdAsync(id).Result;
                if (basketDetail == null)
                {
                    return NotFound();
                }
                _basketDetailService.Remove(basketDetail);

                var product = await _productService.GetByIdAsync(basketDetail.ProductId);
                product.Stock += basketDetail.Quantity;
                _productService.Update(product);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
