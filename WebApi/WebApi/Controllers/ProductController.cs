using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Model;
using WebApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services.ProductService;
using WebApi.Services.ProductGroupService;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService )
        {
            _productService= productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products=await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetAsync(id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDTO productCreateDTO)
        {
           await _productService.AddAsync(productCreateDTO);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductCreateDTO productCreateDTO)
        {

            var product = await _productService.GetAsync(id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }
            try
            {
                await _productService.UpdateAsync(id,productCreateDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try { 
                var deletedProduct = await _productService.DeleteAsync(id); 
                return Ok(deletedProduct); 
            } 
            catch (InvalidOperationException ex) { 
                return BadRequest(ex.Message); 
            }
        }

    }
}
