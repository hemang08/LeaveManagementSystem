using LeaveManagementSystem.Model.Common;
using LeaveManagementSystem.Model.Leave;

namespace LeaveManagementSystem.Data.DBRepository.Leave
{
    public interface ILeaveRepository
    {
        Task<List<LeaveResponseModel>> GetLeaveRequestList(int page, int size, string sort, string dir);
        Task<LeaveResponseModel> GetLeaveRequestById(long id);
        Task<ResponseModel> SaveLeaveRequest(LeaveRequestModel model);
        Task<ResponseModel> DeleteLeaveRequest(long id);
        Task<List<LeaveTypeListModel>> GetLeaveTypeList();
        Task<List<LeaveStatusListModel>> GetLeaveStatusList();
    }
}
