using System.Collections.Generic;
using System.IO;
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
            if (ctx == null) throw new InvalidDataException("Product Repository Must have a DBContext");
            _ctx = ctx;
        }

        public List<Product> ReadAll()
        {
            return _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name,
                    OwnerId = pe.OwnerId
                })
                .ToList();
        }
        
        public List<Product> ReadMyProducts(int userId)
        {
            return _ctx.Products.Where(p => p.OwnerId == userId)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    OwnerId = p.OwnerId
                })
                .ToList();
        }

        public Product GetProduct(int productId)
        {
            return _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name,
                    OwnerId = pe.OwnerId
                })
                .FirstOrDefault(p => p.Id == productId);

        }

        public Product DeleteProduct(int productId)
        {
            var productToDelete = _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name,
                    OwnerId = pe.OwnerId
                })
                .FirstOrDefault(p => p.Id == productId);
            _ctx.Products.Remove(new ProductEntity() {Id = productId});
            _ctx.SaveChanges();
            return productToDelete;
        }

        public Product CreateProduct(Product productToCreate)
        {
            var entity = _ctx.Add(new ProductEntity()
            {
                Name = productToCreate.Name,
                OwnerId = productToCreate.OwnerId

            }).Entity;
            _ctx.SaveChanges();
            return new Product()
            {
                Id = entity.Id,
                Name = entity.Name,
                OwnerId = entity.OwnerId
            };
        }

        public Product UpdateProduct(Product productToUpdate)
        {
            var productEntity = new ProductEntity()
            {
                Id = productToUpdate.Id,
                Name = productToUpdate.Name,
                OwnerId = productToUpdate.OwnerId
            };
            var entity = _ctx.Update(productEntity).Entity;
            _ctx.SaveChanges();
            return new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                OwnerId = entity.OwnerId
            };
        }
    }
}