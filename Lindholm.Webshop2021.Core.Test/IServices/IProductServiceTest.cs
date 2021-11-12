using System.Collections.Generic;
using Lindholm.Webshop2021.Core.IServices;
using Lindholm.Webshop2021.Core.Models;
using Moq;
using Xunit;

namespace Lindholm.Webshop2021.Core.Test.IServices
{
    public class IProductServiceTest
    {
        [Fact]
        public void IProductService_Exists()
        {
            var serviceMock = new Mock<IProductService>();
            Assert.NotNull(serviceMock.Object);
        }

        #region GetAll Tests
        [Fact]
        public void GetAll_WithNoParams_ReturnsListOfProducts()
        {
            var serviceMock = new Mock<IProductService>();
            serviceMock
                .Setup(s => s.GetAll())
                .Returns(new List<Product>());
            Assert.NotNull(serviceMock.Object);
        }
        #endregion

        #region GetSingleProduct Tests
        [Fact]
        public void GetProduct_WithParams_ReturnsSingleProduct()
        {
            var serviceMock = new Mock<IProductService>();
            var productId = 1;
            serviceMock
                .Setup(s => s.GetProduct(productId))
                .Returns(new Product());
            Assert.NotNull(serviceMock.Object);
        }
        #endregion

        #region DeleteProduct Tests
        [Fact]
        public void DeleteProduct_WithParams_ReturnsDeletedProduct()
        {
            var serviceMock = new Mock<IProductService>();
            var productId = 1;
            serviceMock
                .Setup(s => s.DeleteProduct(productId))
                .Returns(new Product());
            Assert.NotNull(serviceMock.Object);
        }
        #endregion
        
    }
}