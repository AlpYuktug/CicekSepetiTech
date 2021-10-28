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
    public class BasketDetailDetailControllerTest
    {
        private readonly Mock<IProductService> _mockProduct;
        private readonly Mock<IBasketService> _mockBasket;
        private readonly Mock<IBasketDetailService> _mockBasketDetail;

        public readonly BasketDetailController _basketDetailController;
        private static IMapper _mapper;

        private List<BasketDetail> basketDetails;

        public BasketDetailDetailControllerTest()
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

            _mockProduct = new Mock<IProductService>();
            _mockBasket = new Mock<IBasketService>();
            _mockBasketDetail = new Mock<IBasketDetailService>();

            _basketDetailController = new BasketDetailController(_mockBasketDetail.Object, _mockBasket.Object, _mockProduct.Object, _mapper);

            basketDetails = new List<BasketDetail>() { 
                new BasketDetail{ Id = 1, Quantity = 1, ProductId = 1, BasketId = 1},
                new BasketDetail{ Id = 2, Quantity = 5, ProductId = 2, BasketId = 2},
            };
        }

        [Fact]
        public async void GetBasketDetail_AcitonExecutes_ReturnOkResultWithBasketDetail()
        {
            //Act
            _mockBasketDetail.Setup(p => p.GetAllAsync()).ReturnsAsync(basketDetails);
            var result = await _basketDetailController.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBasketDetails = Assert.IsAssignableFrom<IEnumerable<BasketDetailDTO>>(okResult.Value);
            Assert.Equal<int>(2, returnBasketDetails.ToList().Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetBasketDetail_IdInvalid_ReturnNotFound(int basketDetailId)
        {
            //Arrange
            BasketDetail basketDetail = null;

            //Act
            _mockBasketDetail.Setup(p => p.GetByIdAsync(basketDetailId)).ReturnsAsync(basketDetail);
            var result = await _basketDetailController.GetById(basketDetailId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetBasketDetail_IdValid_ReturnOkResult(int basketDetailId)
        {
            //Arrange
            var basketDetail = basketDetails.First(x => x.Id == basketDetailId);

            //Act
            _mockBasketDetail.Setup(p => p.GetByIdAsync(basketDetailId)).ReturnsAsync(basketDetail);
            var result = await _basketDetailController.GetById(basketDetailId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnBasketDetail = Assert.IsType<BasketDetailDTO>(okResult.Value);
        }

        [Fact]
        public async void PostBasketDetail_ActionExecutes_ReturnNotFoundBasketId()
        {
            //Arrange
            var basketDetail = basketDetails.First();

            //Act
            _mockBasketDetail.Setup(p => p.AddAsync(basketDetail)).ReturnsAsync(basketDetail);
            var result =  await _basketDetailController.Save(_mapper.Map<BasketDetailDTO>(basketDetail));

            //Assert
            var createdResult = Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
