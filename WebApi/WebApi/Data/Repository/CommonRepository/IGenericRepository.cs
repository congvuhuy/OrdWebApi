namespace WebApi.Data.Repository.CommonRepository
{
    public interface IGenericRepository<T>where T : class
    {
        Task<IEnumerable<T>> GetAllAsyns();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);

    }

}
