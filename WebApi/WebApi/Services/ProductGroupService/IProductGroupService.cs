using WebApi.DTOs;

namespace WebApi.Services.ProductGroupService
{
    public interface IProductGroupService
    {
        Task<IEnumerable<ProductGroupDTO>> GetAllAsync();
        Task<ProductGroupDTO> GetAsync(int id);
        Task AddAsync(ProductGroupCreateDTO productGroupCreateDTO);
        Task UpdateAsync(int id, ProductGroupCreateDTO newProductGroupDTO);
        Task DeleteAsync(int id);
    }
}
