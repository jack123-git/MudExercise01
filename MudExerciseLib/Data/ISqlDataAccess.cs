using System;
using System.Collections.Generic;
using System.Text;
using MudExerciseLib.Model;

namespace MudExerciseLib.Data
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<Solution>> GetAllSoluationsAsync();
    }
}
