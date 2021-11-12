using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lindholm.Webshop2021.Core.IServices;
using Lindholm.Webshop2021.Core.Models;
using Lindholm.Webshop2021.Domain.IRepositories;

namespace Lindholm.Webshop2021.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            if (productRepository == null)
            {
                throw new InvalidDataException("ProductRepository Cannot Be Null");
            }

            _productRepository = productRepository;
        }

        public List<Product> GetAll()
        {
            return _productRepository.ReadAll();
        }

        public Product GetProduct(int productId)
        {
            return _productRepository.GetProduct(productId);
        }

        public Product DeleteProduct(int productId)
        {
            return _productRepository.DeleteProduct(productId);
        }
    }
}