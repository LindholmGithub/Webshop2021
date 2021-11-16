using System.Collections.Generic;
using Lindholm.Webshop2021.Core.Models;

namespace Lindholm.Webshop2021.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> ReadAll();
        List<Product> ReadMyProducts(int userId);
        Product GetProduct(int productId);
        Product DeleteProduct(int productId);
        Product CreateProduct(Product productToCreate);
        Product UpdateProduct(Product productToUpdate);
    }
}