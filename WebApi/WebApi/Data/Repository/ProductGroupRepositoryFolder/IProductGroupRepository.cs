using WebApi.Data.Repository.CommonRepository;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductGroupRepositoryFolder
{
    public interface IProductGroupRepository
    {
        Task<IEnumerable<ProductGroup>> GetAllAsyns();
        Task<ProductGroup> GetByIdAsync(int id);
        Task<int> AddAsync(ProductGroup productGroup);
        Task<int> UpdateAsync(ProductGroup productGroup);
        Task<int> DeleteAsync(int id);
        Task<ProductGroup> GetByNameAsync(string name);
    }
}
