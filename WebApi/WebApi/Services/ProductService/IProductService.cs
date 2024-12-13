using WebApi.DTOs;

namespace WebApi.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetAsync(int id);
        Task <int> AddAsync(ProductCreateDTO productCreateDTO);
        Task <int> UpdateAsync(int id, ProductCreateDTO newProductDTO);
        Task<int> DeleteAsync(int id);
        Task AddMultipleAsync(MultipleProductsCreateDTO multipleProductsCreateDTO);
    }
}
