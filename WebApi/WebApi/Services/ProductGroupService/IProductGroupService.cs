using WebApi.DTOs;

namespace WebApi.Services.ProductGroupService
{
    public interface IProductGroupService
    {
        Task<IEnumerable<ProductGroupDTO>> GetAllAsync();
        Task<ProductGroupDTO> GetAsync(int id);
        Task <int> AddAsync (ProductGroupCreateDTO productGroupCreateDTO);
        Task <int> UpdateAsync(int id, ProductGroupCreateDTO newProductGroupDTO);
        Task <int> DeleteAsync(int id);
    }
}
