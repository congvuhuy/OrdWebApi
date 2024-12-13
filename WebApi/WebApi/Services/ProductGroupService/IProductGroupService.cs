using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Services.ProductGroupService
{
    public interface IProductGroupService
    {
        Task<IEnumerable<ProductGroupDTO>> GetAllAsync();
        Task<ProductGroupDTO> GetAsync(int id);
        Task <ProductGroup> AddAsync (ProductGroupCreateDTO productGroupCreateDTO);
        Task <int> UpdateAsync(int id, ProductGroupCreateDTO newProductGroupDTO);
        Task <int> DeleteAsync(int id);
        
    }
}
