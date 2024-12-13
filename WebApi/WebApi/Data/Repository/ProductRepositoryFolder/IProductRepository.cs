using WebApi.Data.Repository.CommonRepository;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductRepositoryFolder
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<IEnumerable<Product>> GetAllAsyns();
        Task<Product> GetByIdAsync(int id);
        Task<int> AddAsync(Product product);
        Task<int> UpdateAsync(Product product);
        Task<int> DeleteAsync(int id);

    }
}
