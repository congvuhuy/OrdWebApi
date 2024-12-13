using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Services.ProductGroupService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductGroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(ApplicationDbContext context, IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
            _context = context;
        }
        [HttpGet]
        //[Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> getProductGroup()
        {
            var productGroups = await _productGroupService.GetAllAsync();
            return Ok(productGroups);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles="Customer,Admin")]
        public async Task<IActionResult> GetProductGroupById(int id)
        {
           
            var productGroup = await _productGroupService.GetAsync(id);
            return Ok(productGroup);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> PostProductGroup(ProductGroupCreateDTO productGroupCreateDTO)
        {
            try
            {
                var result = await _productGroupService.AddAsync(productGroupCreateDTO);
                if (result !=null)
                {
                    return Ok("Thêm mới thành công");
                }
                return BadRequest("Thêm mới thất bại");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
           
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateProductGroup(int id, ProductGroupCreateDTO productGroupUpdateDTO)
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
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            try
            {
                var result=await _productGroupService.DeleteAsync(id);
                if(result == 1)
                {
                    return Ok("Xoá thành công");
                }
                return BadRequest("Xoá không thành công");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
