using AutoMapper;
using CicekSepetiTech.API.Controllers;
using CicekSepetiTech.API.DTOs;
using CicekSepetiTech.API.Mapping;
using CicekSepetiTech.Core.Models;
using CicekSepetiTech.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CicekSepetiTech.xUnitTest
{
    public class BasketControllerTest
    {

        private readonly Mock<IBasketService> _mockBasket;
        public readonly BasketController _basketController;
        private static IMapper _mapper;

        private List<Basket> baskets;

        public BasketControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _mockBasket = new Mock<IBasketService>();
            _basketController = new BasketController(_mockBasket.Object, _mapper);

            baskets = new List<Basket>() { 
                new Basket{ Id = 1,CustomerId = 1},
                new Basket{ Id = 2,CustomerId = 1}
            };
        }

        [Fact]
        public async void GetBasket_AcitonExecutes_ReturnOkResultWithBasket()
        {
            //Act
            _mockBasket.Setup(p => p.GetAllAsync()).ReturnsAsync(baskets);
            var result = await _basketController.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBaskets = Assert.IsAssignableFrom<IEnumerable<BasketDTO>>(okResult.Value);
            Assert.Equal<int>(2, returnBaskets.ToList().Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetBasket_IdInvalid_ReturnNotFound(int basketId)
        {
            //Arrange
            Basket basket = null;

            //Act
            _mockBasket.Setup(p => p.GetByIdAsync(basketId)).ReturnsAsync(basket);
            var result = await _basketController.GetById(basketId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetBasket_IdValid_ReturnOkResult(int basketId)
        {
            //Arrange
            var basket = baskets.First(x => x.Id == basketId);

            //Act
            _mockBasket.Setup(p => p.GetByIdAsync(basketId)).ReturnsAsync(basket);
            var result = await _basketController.GetById(basketId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBasket = Assert.IsType<BasketDTO>(okResult.Value);
        }

        [Fact]
        public async void PostBasket_ActionExecutes_ReturnCreatedAtAction()
        {
            //Arrange
            var basket = baskets.First();

            //Act
            _mockBasket.Setup(p => p.AddAsync(basket)).ReturnsAsync(basket);
            var result =  await _basketController.Save(_mapper.Map<BasketDTO>(basket));

            //Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public void DeleteBasket_IdInvalid_ReturnNotFound(int basketId)
        {
            //Arrange
            Basket basket = null;

            //Act
            _mockBasket.Setup(p => p.GetByIdAsync(basketId)).ReturnsAsync(basket);
            var resultNotFound = _basketController.Remove(basketId);

            //Assert
            Assert.IsType<NotFoundResult>(resultNotFound);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteBasket_ActionExecutes_ReturnNoContent(int basketId)
        {
            //Arrange
            var basket = baskets.First();

            //Act
            _mockBasket.Setup(p => p.GetByIdAsync(basketId)).ReturnsAsync(basket);
            _mockBasket.Setup(p => p.Remove(basket));
            var resultNoContent = _basketController.Remove(basketId);

            //Assert
            Assert.IsType<NoContentResult>(resultNoContent);
        }
    }
}
