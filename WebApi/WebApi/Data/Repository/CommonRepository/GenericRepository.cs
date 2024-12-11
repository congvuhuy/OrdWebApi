using Microsoft.EntityFrameworkCore;

namespace WebApi.Data.Repository.CommonRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity=await _dbSet.FindAsync(id);
            if (entity != null)
            {
                //_dbSet.Remove(entity);
                //await _context.SaveChangesAsync();
                entity.GetType().GetProperty("IsDeleted")?.SetValue(entity,true);
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsyns()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
