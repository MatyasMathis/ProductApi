using ProductApi.Models.Domain;

namespace ProductApi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetById(Guid id);

        Task<Product> AddProduct(Product product);
    }
}
