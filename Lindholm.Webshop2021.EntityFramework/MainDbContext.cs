using Lindholm.Webshop2021.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lindholm.Webshop2021.EntityFramework
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) {}
        
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

    }
}