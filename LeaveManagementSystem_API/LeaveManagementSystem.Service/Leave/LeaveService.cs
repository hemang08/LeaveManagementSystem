using LeaveManagementSystem.Data.DBRepository.Leave;
using LeaveManagementSystem.Model.Common;
using LeaveManagementSystem.Model.Leave;

namespace LeaveManagementSystem.Service.Leave
{
    public class LeaveService : ILeaveService
    {
        #region Fields
        private readonly ILeaveRepository _leaveRepository;
        #endregion

        #region Constructor
        public LeaveService(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }
        #endregion

        #region Methods
        public Task<ResponseModel> DeleteLeaveRequest(long id)
        {
            return  _leaveRepository.DeleteLeaveRequest(id);
        }

        public Task<LeaveResponseModel> GetLeaveRequestById(long id)
        {
           return _leaveRepository.GetLeaveRequestById(id);
        }

        public Task<List<LeaveResponseModel>> GetLeaveRequestList(int page, int size, string sort, string dir)
        {
           return _leaveRepository.GetLeaveRequestList(page, size, sort, dir);
        }

        public Task<ResponseModel> SaveLeaveRequest(LeaveRequestModel model)
        {
            return _leaveRepository.SaveLeaveRequest(model);
        }

        public Task<List<LeaveTypeListModel>> GetLeaveTypeList()
        {
            return _leaveRepository.GetLeaveTypeList();
        }

        public Task<List<LeaveStatusListModel>> GetLeaveStatusList()
        {
            return _leaveRepository.GetLeaveStatusList();
        }
        #endregion
    }
}
