using Microsoft.EntityFrameworkCore;
using WebApi.Data.Repository.CommonRepository;
using WebApi.Data.Repository.GroupRepository;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductGroupRepository
{
    public class ProductGroupRepository : GenericRepository<ProductGroup>,IProductGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductGroupRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductGroup> GetByNameAsync(string name)
        {
            return await _context.ProductGroups.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
