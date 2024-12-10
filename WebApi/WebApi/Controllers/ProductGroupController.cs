using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductGroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductGroupController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ;
            _mapper = mapper ;
        }
        [HttpGet]
        public async Task<IActionResult> getProductGroup()
        {
            var productGroupList = await _context.ProductGroups.Include(p => p.Products).ToListAsync();
            var productGroupDTO = _mapper.Map<IEnumerable<ProductGroupDTO>>(productGroupList);
            return Ok(productGroupDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductGroupById(int id)
        {
            var product = await _context.ProductGroups.Include(p => p.Products).FirstOrDefaultAsync(p => p.ProductGroupId == id);
            if (product == null)
            {
                return BadRequest("Nhóm sản phẩm không tồn tại");
            }
            var productDTO = _mapper.Map<ProductGroupDTO>(product);
            return Ok(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> PostProductGroup(ProductGroupCreateDTO productGroupCreateDTO)
        {
            if (productGroupCreateDTO == null)
            {
                return BadRequest("Lỗi");
            }
            var productGroup = _mapper.Map<ProductGroup>(productGroupCreateDTO);
            productGroup.CreatedDate = DateTime.Now;
            productGroup.Products = null;

            _context.ProductGroups.Add(productGroup);
            await _context.SaveChangesAsync();

            var newProductGroupDTO = _mapper.Map<ProductGroupDTO>(productGroup);
            return CreatedAtAction(nameof(GetProductGroupById), new { id = productGroup.ProductGroupId }, newProductGroupDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateProductGroup(int id,ProductGroupCreateDTO productGroupUpdateDTO)
        {
            if(productGroupUpdateDTO == null)
            {
                return BadRequest("Lỗi");
            }
            var productGroup= await _context.ProductGroups.FirstOrDefaultAsync(pg=>pg.ProductGroupId==id);
            if (productGroup == null)
            {
                return BadRequest("Nhóm sản phẩm không tồn tại");
            }
            _mapper.Map(productGroupUpdateDTO, productGroup);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductGroup(int id)
        {
            var productGroup=await _context.ProductGroups.FindAsync(id);
            if (productGroup == null)
            {
                return BadRequest("Nhóm sản phẩm không tồn tại");
            }
            productGroup.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
