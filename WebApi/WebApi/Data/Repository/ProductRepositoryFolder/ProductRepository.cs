using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using System.Xml.Linq;
using WebApi.Data.Repository.CommonRepository;
using WebApi.DTOs;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductRepositoryFolder
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _transaction;
        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddAsync(Product product)
        {
            var Sql = "INSERT INTO Product(Name,Price,Quantity,CreatedDate,IsDeleted,ProductGroupId) " +
                "values (@Name,@Price,@Quantity,@CreatedDate,@IsDeleted,@ProductGroupId)";

            return await _dbConnection.ExecuteAsync(Sql, product);
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "UPDATE Product SET IsDeleted = @values WHERE ProductId=@ProductId";
            return await _dbConnection.ExecuteAsync(sql, new { values = true, ProductId = id });
        }

        public async Task<IEnumerable<Product>> GetAllAsyns()
        {
            var Sql = "SELECT * FROM Product";
            return await _dbConnection.QueryAsync<Product>(Sql);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var Sql = "Select * FROM Product Where ProductId=@Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(Sql, new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            var Sql = "Select * FROM Product Where Name=@Name";
            return await _dbConnection.QueryAsync<Product>(Sql, new { Name = name });
        }

        public async Task<int> UpdateAsync(Product product)
        {
                var sql = "UPDATE Product SET Name = @Name, Price = @Price, Quantity = @Quantity, " +
                    "CreatedDate = @CreatedDate, IsDeleted = @IsDeleted, ProductGroupId = @ProductGroupId WHERE ProductId = @ProductId";
            return await _dbConnection.ExecuteAsync(sql,product);
         }
    }
}
