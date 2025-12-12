using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MudExerciseLib.Extension;
using MudExerciseLib.Model;
using System.Data;

namespace MudExerciseLib.Data
{
    public class SqlDataAccess(IConfiguration config) : ISqlDataAccess
    {
        private readonly IConfiguration _config = config;

        public Task<IEnumerable<Solution>> GetAllSoluationsAsync() 
        {
            using var connection = new SqlConnection(_config.GetConnectionString("SysConnection", DeCodeType.Base64));
            var soluations = connection.QueryAsync<Solution>("select * from AppSetting.Solution Order by Name");
            return soluations;

        }
    }
}
