#define DapperParmeter

using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;

namespace MudExerciseLib.Services
{
    public class DapperDataAccessService : IDataAccessService
    {
        private readonly IConfiguration _configuration;

        public DapperDataAccessService(IConfiguration configuration) => _configuration = configuration;

        public async Task<IEnumerable<T>> GetDataAsync<T, P>(string SpName, P parameters, CommandType commandType = CommandType.StoredProcedure, string connectStringId = "SysConnection")
        {
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectStringId));
            return await connection.QueryAsync<T>(SpName, parameters, commandType: commandType);
        }

        public async Task<int> SaveDataAsync<T>(string spName, T parameters, CommandType commandType = CommandType.StoredProcedure, string connectionStringId = "SysConnection")
        {
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionStringId));
            int num = await connection.ExecuteAsync(spName, parameters, commandType: commandType);
            return num;
        }

        #region Solution
        // 取得 Solution資料
        //public async Task<Solution> GetSolutionAsync(string userAccount)
        //{
        //    using var connection = new SqlConnection(_configuration.GetConnectionString("EbizDbConnection", DeCodeType.Base64));
        //    string spName = "Employee.tp_BaseData_Select";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@UserAccount", userAccount, DbType.String, ParameterDirection.Input);
        //    var baseData = (await connection.QueryAsync<BaseData>(spName, parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        //    connection.Close();
        //    return baseData;
        //}
        #endregion
    }
}
