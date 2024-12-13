using System.Data;
using WebApi.Data.Repository.CommonRepository;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductGroupRepositoryFolder
{
    public interface IProductGroupRepository
    {
        Task<IEnumerable<ProductGroup>> GetAllAsyns();
        Task<ProductGroup> GetByIdAsync(int id);
        Task<ProductGroup> AddAsync(ProductGroup productGroup, IDbTransaction transaction);
        Task<int> UpdateAsync(ProductGroup productGroup);
        Task<int> DeleteAsync(int id);
        Task<ProductGroup> GetByNameAsync(string name, IDbTransaction transaction);
    }
}
