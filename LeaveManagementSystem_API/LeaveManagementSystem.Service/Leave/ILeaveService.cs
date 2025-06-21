using LeaveManagementSystem.Model.Common;
using LeaveManagementSystem.Model.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Service.Leave
{
    public interface ILeaveService
    {
        #region Methods
        Task<List<LeaveResponseModel>> GetLeaveRequestList(int page, int size, string sort, string dir);
        Task<LeaveResponseModel> GetLeaveRequestById(long id);
        Task<ResponseModel> SaveLeaveRequest(LeaveRequestModel model);
        Task<ResponseModel> DeleteLeaveRequest(long id);
        Task<List<LeaveTypeListModel>> GetLeaveTypeList();
        Task<List<LeaveStatusListModel>> GetLeaveStatusList();
        #endregion
    }
}
