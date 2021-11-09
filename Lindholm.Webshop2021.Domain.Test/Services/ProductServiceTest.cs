using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lindholm.Webshop2021.Core.IServices;
using Lindholm.Webshop2021.Core.Models;
using Lindholm.Webshop2021.Domain.IRepositories;
using Lindholm.Webshop2021.Domain.Services;
using Moq;
using Xunit;

namespace Lindholm.Webshop2021.Domain.Test.Services
{
    public class ProductServiceTest
    {
        #region ProductService Initialization Tests

        [Fact]
        public void ProductService_IsIProductService()
        {
            var mockRepo = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepo.Object);
            Assert.IsAssignableFrom<IProductService>(productService);
        }

        [Fact]
        public void ProductService_WithNullRepository_ThrowInvalidDataException()
        {
            Assert
                .Throws<InvalidDataException>(() => new ProductService(null));
        }
        
        [Fact]
        public void ProductService_WithNullRepository_ThrowsExceptionWithMessage()
        {
            
            var exception = Assert
                .Throws<InvalidDataException>(() => new ProductService(null));
            Assert.Equal("ProductRepository Cannot Be Null",exception.Message);
        }
        #endregion

        #region ProductService GetAllProducts Tests

        [Fact]
        public void GetAll_NoParams_CallsProductRepositoryOnce()
        {
            //Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepo.Object);
            
            //Act
            productService.GetAll();
            
            //Assert
            mockRepo.Verify(r => r.ReadAll(), Times.Once);
        }
        [Fact]
        public void GetAll_NoParams_ReturnsAllProductsAsList()
        {
            //Arrange
            var expected = new List<Product> {new Product{Id = 1, Name = "Ostekage"}};
            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(r => r.ReadAll())
                .Returns(expected);
            var productService = new ProductService(mockRepo.Object);
            
            //Act
            productService.GetAll();
            
            //Assert
            Assert.Equal(expected,productService.GetAll(),new ProductComparer());
        }
        

        #endregion

        #region ProductService GetProduct Tests
        
        [Fact]
        public void GetProduct_WithParams_CallsProductRepositoryOnce()
        {
            //Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepo.Object);
            var productId = (int) 1;
            
            //Act
            productService.GetProduct(productId);
            
            //Assert
            mockRepo.Verify(r => r.GetProduct(productId), Times.Once);
        }
        
        [Fact]
        public void GetProduct_WithParams_ReturnsSingleProduct()
        {
            //Arrange
            var expected = new Product {Id = 1, Name = "Ostestol"};
            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(r => r.GetProduct(expected.Id))
                .Returns(expected);
            var productService = new ProductService(mockRepo.Object);
            
            //Act
            productService.GetProduct(expected.Id);
            
            //Assert
            Assert.Equal(expected,productService.GetProduct(expected.Id),new ProductComparer());
        }

        #endregion
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Product obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}