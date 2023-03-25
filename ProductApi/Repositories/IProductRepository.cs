using ProductApi.Models.Domain;

namespace ProductApi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> AddProduct(Product product);
    }
}
