using AutoMapper;
using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Data.Repository.ProductRepositoryFolder;
using WebApi.Data.Repository.UnitOfWorkFolder;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Services.ProductGroupService
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductGroupService(IUnitOfWork unitOfWork,IProductRepository productRepository, IProductGroupRepository productGroupRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddAsync(ProductGroupCreateDTO productGroupCreateDTO)
        {
            var ProductGroup = await _productGroupRepository.GetByNameAsync(productGroupCreateDTO.Name);
           
            if (ProductGroup != null)
            {
                throw new InvalidOperationException("Tên nhóm đã tồn tại");
            }
            var productGroup = _mapper.Map<ProductGroup>(productGroupCreateDTO);
            productGroup.CreatedDate = DateTime.Now;
            return await _productGroupRepository.AddAsync(productGroup);
        }

        

        public async Task<int> DeleteAsync(int id)
        {
            var result = await _productGroupRepository.DeleteAsync(id);
            return result;
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

        public async Task<int> UpdateAsync(int id ,ProductGroupCreateDTO newProductGroupDTO)
        {
            
            var existingProductGroup = await _productGroupRepository.GetByIdAsync(id);
            _mapper.Map(newProductGroupDTO, existingProductGroup);
            existingProductGroup.ProductGroupId = id;
            existingProductGroup.Products = null;
            return await _productGroupRepository.UpdateAsync(existingProductGroup);
        }
    }
}
