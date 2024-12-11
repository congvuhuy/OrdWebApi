using WebApi.DTOs;

namespace WebApi.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetAsync(int id);
        Task AddAsync(ProductCreateDTO productCreateDTO);
        Task UpdateAsync(int id, ProductCreateDTO newProductDTO);
        Task<ProductDTO> DeleteAsync(int id);
    }
}
