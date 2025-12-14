using System.Data;

namespace MudExerciseLib.Services
{
    public interface IDataAccessService
    {
        Task<IEnumerable<T>> GetDataAsync<T, P>(string SpName, P parameters, CommandType commandType = CommandType.StoredProcedure, string connectStringId = "SysConnection");

        Task<int> SaveDataAsync<T>(string spName, T parameters, CommandType commandType = CommandType.StoredProcedure, string connectionStringId = "SysConnection");
    }
}
