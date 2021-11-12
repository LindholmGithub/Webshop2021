using System.Collections.Generic;
using Lindholm.Webshop2021.Core.Models;
using Lindholm.Webshop2021.Domain.IRepositories;
using Moq;
using Xunit;

namespace Lindholm.Webshop2021.Domain.Test.IRepositories
{
    public class IProductRepositoryTest
    {
        [Fact]
        public void IProductRepository_Exists()
        {
            var repoMock = new Mock<IProductRepository>();
            Assert.NotNull(repoMock.Object);
        }
        [Fact]
        public void ReadAll_WithNoParams_ReturnsListOfProducts()
        {
            var repoMock = new Mock<IProductRepository>();
            repoMock
                .Setup(r => r.ReadAll())
                .Returns(new List<Product>());
            Assert.NotNull(repoMock.Object.ReadAll());
        }
        [Fact]
        public void GetProduct_WithParams_ReturnsSingleProduct()
        {
            var repoMock = new Mock<IProductRepository>();
            var productId = (int) 1;
            repoMock
                .Setup(r => r.GetProduct(productId))
                .Returns(new Product());
            Assert.NotNull(repoMock.Object.GetProduct(productId));
        }
        [Fact]
        public void DeleteProduct_WithParams_ReturnsDeletedProduct()
        {
            var repoMock = new Mock<IProductRepository>();
            var productId = (int) 1;
            repoMock
                .Setup(r => r.DeleteProduct(productId))
                .Returns(new Product());
            Assert.NotNull(repoMock.Object.DeleteProduct(productId));
        }
    }
}