using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Data.Repository.ProductRepositoryFolder;

namespace WebApi.Data.Repository.UnitOfWorkFolder
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IProductGroupRepository ProductGroupRepository { get; }
        Task BeginTransactionAsync();
        Task CompleteAsync();
        Task RollbackAsync();
    }
}
