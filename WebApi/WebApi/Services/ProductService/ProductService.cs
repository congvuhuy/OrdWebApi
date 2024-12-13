using AutoMapper;
using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Data.Repository.ProductRepositoryFolder;
using WebApi.Data.Repository.UnitOfWorkFolder;
using WebApi.DTOs;
using WebApi.Model;
using WebApi.Services.ProductGroupService;

namespace WebApi.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupService _productGroupService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork,IProductRepository productRepository,IProductGroupService productGroupService ,IMapper mapper) {
            _productRepository= productRepository;
            _productGroupService = productGroupService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddAsync(ProductCreateDTO productCreateDTO)
        {
            var productGroup = await _productGroupService.GetAsync(productCreateDTO.ProductGroupId);
            if (productGroup == null)
            {
                throw new InvalidOperationException("Nhóm sản phẩm không tồn tại");
            }
            var exitProducts = await _productRepository.GetByNameAsync(productCreateDTO.Name);
            foreach(var exitProduct in exitProducts)
            {
                if (exitProduct.ProductGroupId == productCreateDTO.ProductGroupId)
                {
                    throw new InvalidOperationException("Nhóm đã tồn tại tên sản phẩm này");
                }
            }
            var product = _mapper.Map<Product>(productCreateDTO); 
            return await _productRepository.AddAsync(product);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var result = await _unitOfWork.ProductRepository.DeleteAsync(id); 
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
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("Sản phẩm không tồn tại");
            }
            var productGroup = await _productGroupService.GetAsync(newProductDTO.ProductGroupId);
            if (productGroup == null)
            {
                throw new InvalidOperationException("Nhóm sản phẩm không tồn tại");
            }
            _mapper.Map(newProductDTO, existingProduct);
            existingProduct.ProductId = id;
            return await  _unitOfWork.ProductRepository.UpdateAsync(existingProduct);
        }
        public async Task AddMultipleAsync(MultipleProductsCreateDTO multipleProductsCreateDTO)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                    var productGroup = new ProductGroup
                    {
                        Name = multipleProductsCreateDTO.ProductGroups.Name,
                        Description = multipleProductsCreateDTO.ProductGroups.Description,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = multipleProductsCreateDTO.ProductGroups.IsDeleted
                    };
                    var exitingGroup = await _unitOfWork.ProductGroupRepository.GetByNameAsync(productGroup.Name);

                    if (exitingGroup != null)
                    {
                        throw new InvalidOperationException("Tên nhóm đã tồn tại");
                    }
                    await _unitOfWork.ProductGroupRepository.AddAsync(productGroup);

                    // Thêm các sản phẩm liên quan tới nhóm sản phẩm này
                    //var products = multipleProductsCreateDTO.Products.Where(p => p.ProductGroupId == productGroup.ProductGroupId).ToList();
                    foreach (var productDto in multipleProductsCreateDTO.Products)
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
                        var exitProducts = await _productRepository.GetByNameAsync(product.Name);
                        foreach (var exitProduct in exitProducts)
                        {
                            if (exitProduct.ProductGroupId == product.ProductGroupId)
                            {
                                throw new InvalidOperationException("Trùng tên sản phẩm");
                            }
                        }
                        await _unitOfWork.ProductRepository.AddAsync(product);
                    }
                

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
