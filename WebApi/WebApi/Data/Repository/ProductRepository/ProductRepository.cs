using Microsoft.EntityFrameworkCore;
using WebApi.Data.Repository.CommonRepository;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
