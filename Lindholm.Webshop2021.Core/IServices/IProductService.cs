using Lindholm.Webshop2021.Core.Models;
using System.Collections.Generic;


namespace Lindholm.Webshop2021.Core.IServices
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetMyProducts(int userId);
        Product GetProduct(int productId);
        Product DeleteProduct(int productId);
        Product CreateProduct(Product productToCreate);
        Product UpdateProduct(Product product);
    }
}