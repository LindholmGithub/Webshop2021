using Lindholm.Webshop2021.Core.Models;
using Xunit;

namespace Lindholm.Webshop2021.Core.Test.Models
{
    public class ProductTest
    {
        [Fact]
        public void Product_Exists()
        {
            var product = new Product();
            Assert.NotNull(product);
        }

        [Fact]
        public void Product_HasIntProperty_Id()
        {
            var product = new Product();
            product.Id = (int) 1;
            Assert.Equal(1,product.Id);
        }
        
        [Fact]
        public void Product_HasStringProperty_Name()
        {
            var expected = "Cheese";
            var product = new Product();
            product.Name = expected;
            Assert.Equal(expected,product.Name);
        }
    }
}