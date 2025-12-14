using Microsoft.Extensions.Configuration;
using MudExerciseLib.Data;
using MudExerciseLib.Models;
using MudExerciseLib.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MudExerciseLib.Repository
{
    public class DbRepository: IDbRepository
    {
        private readonly IDataAccessService dataAccessService;

        public DbRepository(IDataAccessService dataAccessService) => this.dataAccessService = dataAccessService;

        #region Solution
        /// <summary>
        /// 取得資料庫方案清單
        /// </summary>
        /// <returns>IEnumerable<Solution></returns>
        public async Task<IEnumerable<Solution>> GetAllSoluationsAsync()
        {
            string sql = " select SolutionId,SolutionName,Enabled from AppData.Solution Order by SolutionId";
            var result = await dataAccessService.GetDataAsync<Solution, dynamic>(sql, new { }, System.Data.CommandType.Text);
            return result;
        }


        /// <summary>
        /// 取得Solution資料
        /// </summary>
        /// <param name="id">SolutionID</param>
        /// <returns></returns>
        public async Task<Solution?> GetSolutionByIdAsync(int id)
        {
            string storedProcedure = "tp_Solution_Select";
            var result = await dataAccessService.GetDataAsync<Solution, dynamic>(storedProcedure, new { SolutionId = id });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// 新增Solution資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<CommadResult> AddSolutionAsync(Solution data)
        {
            var result = new CommadResult();
            try
            {
                string storedProcedure = "tp_Solution_Insert";
                result.ResultCode = await dataAccessService.SaveDataAsync(storedProcedure, new
                {
                    SolutionId = data.SolutionId,
                    SolutionName = data.SolutionName,
                    Enabled = data.Enabled
                });
                result.CommandStatus = 1;
            }
            catch (Exception ex)
            {
                result.ResultCode = 0;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 更新Solution資料
        /// </summary>
        /// <param name="data">Solution資料</param>
        /// <returns></returns>
        public async Task<CommadResult> UpdateSolutionAsync(Solution data)
        {
            var result = new CommadResult();
            string storedProcedure = "tp_Solution_Update";

            try
            {
                result.ResultCode = await dataAccessService.SaveDataAsync(storedProcedure, new
                {
                    SolutionId = data.SolutionId,
                    SolutionName = data.SolutionName,
                    Enabled = data.Enabled
                });
                result.CommandStatus = 1;
            }
            catch (Exception ex)
            {
                result.ResultCode = 0;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 刪除Solution資料
        /// </summary>
        /// <param name="id">SolutionId</param>
        /// <returns></returns>
        public async Task<CommadResult> DeleteSolutionAsync(int id)
        {
            var result = new CommadResult();
            try
            {
                result.ResultCode = await dataAccessService.SaveDataAsync("tp_Solution_Delete", new { SolutionId = id });
                result.CommandStatus = 1;
            }
            catch (Exception ex)
            {
                result.ResultCode = 0;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #endregion Solution
    }
}
