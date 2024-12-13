using MySqlConnector;
using System.Data;
using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Data.Repository.ProductRepositoryFolder;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Data.Repository.UnitOfWorkFolder
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public IProductRepository ProductRepository { get; }
        public IProductGroupRepository ProductGroupRepository { get; }

        public UnitOfWork(IDbConnection connection, IProductRepository productRepository, IProductGroupRepository productGroupRepository)
        {
            _connection = connection; 
            ProductRepository = productRepository; 
            ProductGroupRepository = productGroupRepository;
        }
        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                 _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
            await Task.CompletedTask;
        }

        public async Task CompleteAsync()
        {
            _transaction.Commit(); 
            _connection.Close(); 
            await Task.CompletedTask;
        }

        public async Task RollbackAsync()
        {
            _transaction.Rollback(); 
            _connection.Close(); 
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
