using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models.Domain;

namespace ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductApiDbContext dbContext;
        public ProductRepository(ProductApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();
           await dbContext.AddAsync(product);
           await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
   

