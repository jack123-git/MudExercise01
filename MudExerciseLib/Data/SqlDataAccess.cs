using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MudExerciseLib.Extension;
using MudExerciseLib.Models;
using System.Data;

namespace MudExerciseLib.Data
{
    public class SqlDataAccess(IConfiguration config) : ISqlDataAccess
    {
        private readonly IConfiguration _config = config;

        public Task<IEnumerable<Solution>> GetAllSoluationsAsync() 
        {
            //using var connection = new SqlConnection(_config.GetConnectionString("SysConnection", DeCodeType.Base64));
            using var connection = new SqlConnection(_config.GetConnectionString("SysConnection", DeCodeType.NONE));
            var soluations = connection.QueryAsync<Solution>("select * from AppData.Solution Order by SolutionName");
            return soluations;

        }
    }
}
