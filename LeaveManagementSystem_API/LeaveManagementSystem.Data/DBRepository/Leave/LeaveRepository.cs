using Dapper;
using LeaveManagementSystem.Common.Helpers;
using LeaveManagementSystem.Model.Common;
using LeaveManagementSystem.Model.Leave;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LeaveManagementSystem.Data.DBRepository.Leave
{
    internal class LeaveRepository : ILeaveRepository
    {

        private readonly IConfiguration _config;

        public LeaveRepository(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection => new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);

        public async Task<ResponseModel> DeleteLeaveRequest(long id)
        {
            using var db = Connection;
            var param = new DynamicParameters();

            param.Add("@LeaveRequestId", id);

             var result = await db.QueryFirstOrDefaultAsync<ResponseModel>(StoredProcedures.DeleteLeaveRequest, param, commandType: CommandType.StoredProcedure);
            return result;

        }

        public async Task<LeaveResponseModel> GetLeaveRequestById(long id)
        {
            using var db = Connection;
            var param = new DynamicParameters();
            param.Add("@LeaveRequestId", id);


            var result = await db.QueryFirstOrDefaultAsync<LeaveResponseModel>(StoredProcedures.GetLeaveRequestById,param,commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<List<LeaveResponseModel>> GetLeaveRequestList(int page, int size, string sort, string dir)
        {
            using var db = Connection;

            var param = new DynamicParameters();
            param.Add("@Page", page);
            param.Add("@PageSize", size);
            param.Add("@SortColumn", sort);
            param.Add("@SortDirection", dir);


            var result = await db.QueryAsync<LeaveResponseModel>(StoredProcedures.GetLeaveRequestList,param,commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        public async Task<ResponseModel> SaveLeaveRequest(LeaveRequestModel model)
        {
            using var db = Connection;
            var param = new DynamicParameters();

            param.Add("@LeaveRequestId", model.LeaveRequestId);
            param.Add("@EmployeeName", model.EmployeeName);
            param.Add("@LeaveTypeId", model.LeaveTypeId);
            param.Add("@FromDate", model.FromDate);
            param.Add("@ToDate", model.ToDate);
            param.Add("@StatusId", model.StatusId);


           var result = await db.QueryFirstOrDefaultAsync<ResponseModel>(StoredProcedures.SaveLeaveRequest, param, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<List<LeaveTypeListModel>> GetLeaveTypeList()
        {
            using var db = Connection;
            var result = await db.QueryAsync<LeaveTypeListModel>(StoredProcedures.GetLeaveType, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<List<LeaveStatusListModel>> GetLeaveStatusList()
        {
            using var db = Connection;
            var result = await db.QueryAsync<LeaveStatusListModel>(StoredProcedures.GetLeaveStatus, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
}
