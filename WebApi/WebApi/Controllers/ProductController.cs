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
using Microsoft.AspNetCore.Authorization;

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
        //[Authorize(Roles="Customer,Admin")]
        public async Task<IActionResult> GetProduct()
        {
            var products=await _productService.GetAllAsync();
            return Ok(products);
        }
        //[Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetAsync(id);
            return Ok(product);
        }
        //[Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDTO productCreateDTO)
        {
            try
            {
                var result = await _productService.AddAsync(productCreateDTO);
                if (result == 0)
                {
                    return BadRequest("Thêm mới không thành công");
                }
                //return CreatedAtAction(nameof(GetProductById), new { id = productCreateDTO.ProductGroupId }, productCreateDTO);
                return Ok("Thêm mới thành công");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        //[Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductCreateDTO productCreateDTO)
        {
            try
            {
                var result = await _productService.UpdateAsync(id, productCreateDTO);
                if (result == 1)
                {
                    return NoContent();
                }
                return NotFound();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        //[Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try { 
                var result = await _productService.DeleteAsync(id); 
                if (result == 1) {
                    return Ok("Xoá thành công");
                }
                return BadRequest("Xoá không thành công");
              
            } 
            catch (InvalidOperationException ex) { 
                return BadRequest(ex.Message); 
            }
        }
        [HttpPost("add-multiple")]
        public async Task<IActionResult> AddMultiple([FromBody] MultipleProductsCreateDTO dto)
        {
            try
            {
                await _productService.AddMultipleAsync(dto);
                return Ok("Thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
