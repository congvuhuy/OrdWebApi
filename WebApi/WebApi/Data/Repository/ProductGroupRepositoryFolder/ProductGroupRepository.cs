using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using WebApi.Data.Repository.CommonRepository;
using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Model;

namespace WebApi.Data.Repository.ProductGroupRepositoryFolder
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly IDbConnection _dbConnection;
        public ProductGroupRepository(IConfiguration configuration)
        {
            _dbConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> AddAsync(ProductGroup productGroup)
        {
            var Sql = "INSERT INTO ProductGroup(Name,Description,CreatedDate,IsDeleted) " +
              "values (@Name,@Description,@CreatedDate,@IsDeleted)";

            return await _dbConnection.ExecuteAsync(Sql, productGroup);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "UPDATE ProductGroup SET IsDeleted = @values WHERE ProductGroupId=@ProductGroupId";
            return await _dbConnection.ExecuteAsync(sql, new { values = true, ProductGroupId = id });
        }

        public async Task<IEnumerable<ProductGroup>> GetAllAsyns()
        {
            var Sql = "SELECT * FROM ProductGroup";
            return await _dbConnection.QueryAsync<ProductGroup>(Sql);
        }

        public async Task<ProductGroup> GetByIdAsync(int id)
        {
            var Sql = "Select * FROM ProductGroup Where ProductGroupId=@Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<ProductGroup>(Sql, new { Id = id });
        }

        public async Task<ProductGroup> GetByNameAsync(string name)
        {
            var Sql = "Select * FROM ProductGroup Where Name=@Name";
            return await _dbConnection.QueryFirstOrDefaultAsync<ProductGroup>(Sql, new { Name = name });
        }

        public async Task<int> UpdateAsync(ProductGroup productGroup)
        {
            var sql = "UPDATE ProductGroup SET Name = @Name,Desciption=@Description" +
                    "CreatedDate = @CreatedDate, IsDeleted = @IsDeleted  WHERE ProductGroupId = @ProductGroupId";
            return await _dbConnection.ExecuteAsync(sql, productGroup);
        }
    }
}
