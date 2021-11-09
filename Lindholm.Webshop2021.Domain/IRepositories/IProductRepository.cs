using System.Collections.Generic;
using Lindholm.Webshop2021.Core.Models;

namespace Lindholm.Webshop2021.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> ReadAll();
        Product GetProduct(int productId);
    }
}