using Lindholm.Webshop2021.Core.Models;
using System.Collections.Generic;


namespace Lindholm.Webshop2021.Core.IServices
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetProduct(int productId);
    }
}