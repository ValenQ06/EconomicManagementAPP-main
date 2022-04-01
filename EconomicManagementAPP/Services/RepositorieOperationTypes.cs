using EconomicManagementAPP.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using EconomicManagementAPP.IRepositorie;

namespace EconomicManagementAPP.Services
{
    public class RepositorieOperationTypes : IRepositorieOperationTypes
    {
        private readonly string connectionString;

        public RepositorieOperationTypes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(OperationTypes operationTypes)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO OperationTypes 
                                                (Description) 
                                                VALUES (@Description); SELECT SCOPE_IDENTITY();", operationTypes);
            operationTypes.Id = id;
        }

        public async Task<bool> Exist(string Description)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                                    @"SELECT 1
                                    FROM OperationTypes
                                    WHERE Description = @Description;",
                                    new { Description });
            return exist == 1;
        }

        public async Task<IEnumerable<OperationTypes>> getOperationTypes()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<OperationTypes>(@"SELECT Id, Description FROM OperationTypes");
        }

        public async Task Modify(OperationTypes operationTypes)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE OperationTypes
                                            SET Description = @Description
                                            WHERE Id = @Id", operationTypes);
        }

        public async Task<OperationTypes> getOperationTypesById(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<OperationTypes>(@"
                                                                SELECT Description
                                                                FROM OperationTypes
                                                                WHERE Id = @Id",
                                                                new { Id });
        }

        public async Task Delete(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE OperationTypes WHERE Id = @Id", new { Id });
        }
    }
}
