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

        public async Task<Product> DeleteById(Guid id)
        {
            var productToDelete = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productToDelete == null)
            {
                return null;
            }

            dbContext.Remove(productToDelete);
            await dbContext.SaveChangesAsync();
            return productToDelete;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> UpdateProduct(Guid Id, Product product)
        {
            var productExisting = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (productExisting == null)
            {
                return null;

            }
            productExisting.Name = product.Name;
            productExisting.Description= product.Description;
            productExisting.Price= product.Price;

            await dbContext.SaveChangesAsync();
            return productExisting;
        }
    }
}
   

