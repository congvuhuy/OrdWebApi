using AutoMapper;
using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Data.Repository.ProductRepositoryFolder;
using WebApi.Data.Repository.UnitOfWorkFolder;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork,IProductRepository productRepository,IProductGroupRepository productGroupRepository ,IMapper mapper) {
            _productRepository= productRepository;
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddAsync(ProductCreateDTO productCreateDTO)
        {
            var productGroup = await _productGroupRepository.GetByIdAsync(productCreateDTO.ProductGroupId);
            if (productGroup == null)
            {
                throw new InvalidOperationException("Nhóm sản phẩm không tồn tại");
            }
            var product = _mapper.Map<Product>(productCreateDTO); 
            return await _productRepository.AddAsync(product);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var result = await _productRepository.DeleteAsync(id); 
            if (result == 0) { 
                throw new InvalidOperationException("Sản phẩm không tồn tại hoặc đã bị xóa"); 
            }      
            return result;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products=await _productRepository.GetAllAsyns();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var prouduct=await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(prouduct);
        }

        public async Task<int> UpdateAsync(int id ,ProductCreateDTO newProductDTO)
        {
            var productGroup = await _productGroupRepository.GetByIdAsync(newProductDTO.ProductGroupId);
            if (productGroup == null)
            {
                throw new InvalidOperationException("Nhóm sản phẩm không tồn tại");
            }
            var existingProduct = await _productRepository.GetByIdAsync(id);
            _mapper.Map(newProductDTO, existingProduct);
            existingProduct.ProductId = id;
            return await _productRepository.UpdateAsync(existingProduct);
        }
        public async Task AddMultipleAsync(MultipleProductsCreateDTO multipleProductsCreateDTO)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Thêm từng nhóm sản phẩm
                foreach (var productGroupDto in multipleProductsCreateDTO.ProductGroups)
                {
                    var productGroup = new ProductGroup
                    {
                        Name = productGroupDto.Name,
                        Description = productGroupDto.Description,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = productGroupDto.IsDeleted
                    };
                    await _unitOfWork.ProductGroupRepository.AddAsync(productGroup);

                    // Thêm các sản phẩm liên quan tới nhóm sản phẩm này
                    var products = multipleProductsCreateDTO.Products.Where(p => p.ProductGroupId == productGroup.ProductGroupId).ToList();
                    foreach (var productDto in products)
                    {
                        var product = new Product
                        {
                            Name = productDto.Name,
                            Price = productDto.Price,
                            Quantity = productDto.Quantity,
                            CreatedDate = DateTime.UtcNow,
                            IsDeleted = productDto.IsDeleted,
                            ProductGroupId = productGroup.ProductGroupId
                        };
                        await _unitOfWork.ProductRepository.AddAsync(product);
                    }
                }

                // Commit transaction
                await _unitOfWork.CompleteAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
