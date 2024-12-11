using AutoMapper;
using WebApi.Data.Repository.GroupRepository;
using WebApi.Data.Repository.ProductRepository;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Services.ProductGroupService
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        public ProductGroupService(IProductRepository productRepository, IProductGroupRepository productGroupRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(ProductGroupCreateDTO productGroupCreateDTO)
        {
            var ProductGroup = _productGroupRepository.GetByNameAsync(productGroupCreateDTO.Name);
            if (ProductGroup != null)
            {
                throw new InvalidOperationException("Tên nhóm đã tồn tại");
            }
            var productGroup = _mapper.Map<ProductGroup>(productGroupCreateDTO);
            productGroup.CreatedDate = DateTime.Now;
            productGroup.Products = null;
            await _productGroupRepository.AddAsync(productGroup);
        }

        public async Task DeleteAsync(int id)
        {
            var productGroup = await _productGroupRepository.DeleteAsync(id);
            if (productGroup != null)
            {
                throw new InvalidOperationException("Nhóm muốn xoá không tồn tại hoặc đã bị xoá");
            }

            _mapper.Map<ProductGroupDTO>(productGroup);
        }

        public async Task<IEnumerable<ProductGroupDTO>> GetAllAsync()
        {
            var productGroups = await _productGroupRepository.GetAllAsyns();
            return _mapper.Map<IEnumerable<ProductGroupDTO>>(productGroups);
        }

        public async Task<ProductGroupDTO> GetAsync(int id)
        {
           var productGroup =await _productGroupRepository.GetByIdAsync(id);
            return  _mapper.Map<ProductGroupDTO>(productGroup);
        }

        public async Task UpdateAsync(int id ,ProductGroupCreateDTO newProductGroupDTO)
        {
            
            var existingProductGroup = await _productGroupRepository.GetByIdAsync(id);
            _mapper.Map(newProductGroupDTO, existingProductGroup);
            existingProductGroup.ProductGroupId = id;
            await _productGroupRepository.UpdateAsync(existingProductGroup);
            _mapper.Map<ProductGroupDTO>(existingProductGroup);

        }
    }
}
