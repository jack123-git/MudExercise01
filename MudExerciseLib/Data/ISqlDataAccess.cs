using System;
using System.Collections.Generic;
using System.Text;
using MudExerciseLib.Models;

namespace MudExerciseLib.Data
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<Solution>> GetAllSoluationsAsync();
    }
}
