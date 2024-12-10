using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Model;
using WebApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var productList = await _context.Products.Include(p => p.ProductGroup).ToListAsync();
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(productList);
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.Include(p => p.ProductGroup).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDTO productCreateDTO)

        {
            if (productCreateDTO == null)
            {
                return BadRequest("Lỗi");
            }

            var productGroup = await _context.ProductGroups.FindAsync(productCreateDTO.ProductGroupId);
            if (productGroup == null)
            {
                return BadRequest("Product Group không tồn tại");
            }

            var product = _mapper.Map<Product>(productCreateDTO);
            product.CreatedDate = DateTime.Now;
            product.ProductGroup = productGroup;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var newProductDTO = _mapper.Map<ProductDTO>(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, newProductDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductCreateDTO productCreateDTO)
        {
            if (productCreateDTO == null) {
                return BadRequest("Lỗi");
            }
            var product = await _context.Products.Include(p => p.ProductGroup).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) {
                return NotFound("Sản phẩm không tồn tại.");
            }
            var productGroup = await _context.ProductGroups.FindAsync(productCreateDTO.ProductGroupId);
            if (productGroup == null) {
                return BadRequest("Product Group không tồn tại.");
            }
            _mapper.Map(productCreateDTO, product);
            product.ProductGroup = productGroup;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return BadRequest("Sản phẩm không tồn tại");
            }
            product.IsDeleted= true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
