using System.Data;
using WebApi.Data.Repository.CommonRepository;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductRepositoryFolder
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByNameAsync(string name,IDbTransaction transaction);
        Task<IEnumerable<Product>> GetAllAsyns();
        Task<Product> GetByIdAsync(int id);
        Task<int> AddAsync(Product product, IDbTransaction transaction);
        Task<int> UpdateAsync(Product product);
        Task<int> DeleteAsync(int id);

    }
}
