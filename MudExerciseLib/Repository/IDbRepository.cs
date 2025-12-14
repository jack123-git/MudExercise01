using MudExerciseLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MudExerciseLib.Repository
{
    public interface IDbRepository
    {
        #region Solution
        Task<IEnumerable<Solution>> GetAllSoluationsAsync();
        Task<Solution?> GetSolutionByIdAsync(int id);
        Task<CommadResult> AddSolutionAsync(Solution data);
        Task<CommadResult> UpdateSolutionAsync(Solution data);
        Task<CommadResult> DeleteSolutionAsync(int id);
        #endregion Solution
    }
}
