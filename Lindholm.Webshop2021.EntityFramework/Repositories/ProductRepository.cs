using System.Collections.Generic;
using System.Linq;
using Lindholm.Webshop2021.Core.Models;
using Lindholm.Webshop2021.Domain.IRepositories;
using Lindholm.Webshop2021.EntityFramework.Entities;

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
            return _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name
                })
                .FirstOrDefault(p => p.Id == productId);

        }

        public Product DeleteProduct(int productId)
        {
            var productToDelete = _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name
                })
                .FirstOrDefault(p => p.Id == productId);
            _ctx.Products.Remove(new ProductEntity() {Id = productId});
            _ctx.SaveChanges();
            return productToDelete;
        }
    }
}