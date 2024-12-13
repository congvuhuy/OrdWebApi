using MySqlConnector;
using System.Data;
using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Data.Repository.ProductRepositoryFolder;

namespace WebApi.Data.Repository.UnitOfWorkFolder
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IProductRepository _productRepository;
        private readonly IProductGroupRepository _productGroupRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _productRepository = new ProductRepository(configuration);
            _productGroupRepository = new ProductGroupRepository(configuration);
        }

        public IProductRepository ProductRepository => _productRepository;
        public IProductGroupRepository ProductGroupRepository => _productGroupRepository;

        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                 _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }

        public async Task CompleteAsync()
        {
            await Task.Run(() => _transaction.Commit());
            _connection.Close();
        }

        public async Task RollbackAsync()
        {
            await Task.Run(() => _transaction.Rollback());
            _connection.Close();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
