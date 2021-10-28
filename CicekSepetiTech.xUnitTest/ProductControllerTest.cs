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
    public class ProductControllerTest
    {

        private readonly Mock<IProductService> _mockProduct;
        public readonly ProductController _productController;
        private static IMapper _mapper;

        private List<Product> products;

        public ProductControllerTest()
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
            _productController = new ProductController(_mockProduct.Object, _mapper);

            products = new List<Product>() { 
                new Product{ Id = 1,Name = "Rose"},
                new Product{ Id = 2,Name = "Daisy"}
            };
        }

        [Fact]
        public async void GetProduct_AcitonExecutes_ReturnOkResultWithProduct()
        {
            //Act
            _mockProduct.Setup(p => p.GetAllAsync()).ReturnsAsync(products);
            var result = await _productController.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProducts = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(okResult.Value);
            Assert.Equal<int>(2, returnProducts.ToList().Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetProduct_IdInvalid_ReturnNotFound(int productId)
        {
            //Arrange
            Product product = null;

            //Act
            _mockProduct.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);
            var result = await _productController.GetById(productId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetProduct_IdValid_ReturnOkResult(int productId)
        {
            //Arrange
            var product = products.First(x => x.Id == productId);

            //Act
            _mockProduct.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);
            var result = await _productController.GetById(productId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProduct = Assert.IsType<ProductDTO>(okResult.Value);
        }

        [Fact]
        public async void PostProduct_ActionExecutes_ReturnCreatedAtAction()
        {
            //Arrange
            var product = products.First();

            //Act
            _mockProduct.Setup(p => p.AddAsync(product)).ReturnsAsync(product);
            var result =  await _productController.Save(_mapper.Map<ProductDTO>(product));

            //Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public void DeleteProduct_IdInvalid_ReturnNotFound(int productId)
        {
            //Arrange
            Product product = null;

            //Act
            _mockProduct.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);
            var resultNotFound = _productController.Remove(productId);

            //Assert
            Assert.IsType<NotFoundResult>(resultNotFound);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteProduct_ActionExecutes_ReturnNoContent(int productId)
        {
            //Arrange
            var product = products.First();

            //Act
            _mockProduct.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockProduct.Setup(p => p.Remove(product));
            var resultNoContent = _productController.Remove(productId);

            //Assert
            Assert.IsType<NoContentResult>(resultNoContent);
        }
    }
}
