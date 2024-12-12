﻿using AutoMapper;
using WebApi.Data.Repository.GroupRepository;
using WebApi.Data.Repository.ProductGroupRepository;
using WebApi.Data.Repository.ProductRepository;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,IProductGroupRepository productGroupRepository ,IMapper mapper) {
            _productRepository= productRepository;
            _productGroupRepository = productGroupRepository;
            _mapper = mapper;
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
    }
}
