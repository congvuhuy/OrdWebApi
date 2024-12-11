using WebApi.Data.Repository.CommonRepository;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductRepository
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<Product> GetByNameAsync(string name);
    }
}
