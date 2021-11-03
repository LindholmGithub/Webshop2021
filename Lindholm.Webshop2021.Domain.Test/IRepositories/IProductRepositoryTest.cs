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
    }
}