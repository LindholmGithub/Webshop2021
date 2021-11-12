using System.Collections.Generic;
using System.Linq;
using Lindholm.Webshop2021.Core.Models;
using Lindholm.Webshop2021.Domain.IRepositories;

namespace Lindholm.Webshop2021.EntityFramework.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MainDbContext _ctx;
        public ProductRepository(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Product> ReadAll()
        {
            return _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name
                })
                .ToList();
        }

        public Product GetProduct(int productId)
        {
            throw new System.NotImplementedException();
        }
    }
}