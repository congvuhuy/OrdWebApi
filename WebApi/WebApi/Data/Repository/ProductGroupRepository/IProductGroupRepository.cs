using WebApi.Data.Repository.CommonRepository;
using WebApi.Model;

namespace WebApi.Data.Repository.GroupRepository
{
    public interface IProductGroupRepository:IGenericRepository<ProductGroup>
    {
        Task<ProductGroup> GetByNameAsync(string name);
    }
}
