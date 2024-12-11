using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Model;
using WebApi.Services.ProductGroupService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductGroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(ApplicationDbContext context,ProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }
        [HttpGet]
        public async Task<IActionResult> getProductGroup()
        {
            var productGroups=await _productGroupService.GetAllAsync();
            return Ok(productGroups);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductGroupById(int id)
        {
            var productGroup = await _productGroupService.GetAsync(id);
            return Ok(productGroup);
        }
        [HttpPost]
        public async Task<IActionResult> PostProductGroup(ProductGroupCreateDTO productGroupCreateDTO)
        {
            await _productGroupService.AddAsync(productGroupCreateDTO);
            return Ok("Thêm mới thành công");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateProductGroup(int id,ProductGroupCreateDTO productGroupUpdateDTO)
        {
            var product = await _productGroupService.GetAsync(id);
            if (product == null)
            {
                return NotFound("Nhóm sản phẩm không tồn tại.");
            }
            try
            {
                await _productGroupService.UpdateAsync(id, productGroupUpdateDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            try
            {
                await _productGroupService.DeleteAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
