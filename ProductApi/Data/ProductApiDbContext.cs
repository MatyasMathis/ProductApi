using Microsoft.EntityFrameworkCore;
using ProductApi.Models.Domain;

namespace ProductApi.Data
{
    public class ProductApiDbContext:DbContext
    {
        public ProductApiDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
