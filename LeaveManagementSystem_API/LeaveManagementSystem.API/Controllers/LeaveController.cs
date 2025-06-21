using LeaveManagementSystem.Common.Helpers;
using LeaveManagementSystem.Model.Leave;
using LeaveManagementSystem.Service.Leave;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static LeaveManagementSystem.Common.Helpers.Enum;

namespace LeaveManagementSystem.API.Controllers
{
    [Route("api/leave")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        #region Fields
        private readonly ILeaveService _leaveService;
        #endregion

        #region Constuctor
        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }
        #endregion

        #region Methods
        #endregion


        [HttpPost]
        public async Task<BaseApiResponse> SaveLeaveRequest([FromBody] LeaveModel request)
        {
            BaseApiResponse response = new();
            LeaveRequestModel model = new LeaveRequestModel
            {
                LeaveRequestId = 0,
                EmployeeName = request.EmployeeName,
                LeaveTypeId = request.LeaveTypeId,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                StatusId = request.StatusId,
            };

            var result = await _leaveService.SaveLeaveRequest(model);

            response.Success = result.Success == (int)ResponseStatus.Success;
            response.Message = result.Message;
            return response;
        }

        [HttpPut("{id}")]
        public async Task<BaseApiResponse> UpdateLeaveRequest(long id, [FromBody] LeaveModel request)
        {
            BaseApiResponse response = new();
            LeaveRequestModel model = new LeaveRequestModel
            {
                LeaveRequestId = id,
                EmployeeName = request.EmployeeName,
                LeaveTypeId = request.LeaveTypeId,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                StatusId = request.StatusId,
            };

            var result = await _leaveService.SaveLeaveRequest(model);

            response.Success = result.Success == (int)ResponseStatus.Success;
            response.Message = result.Message;
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<BaseApiResponse> DeleteLeaveRequest(long id)
        {
            BaseApiResponse response = new();
            var result = await _leaveService.DeleteLeaveRequest(id);

            response.Success = result.Success == (int)ResponseStatus.Success;
            response.Message = result.Message;
            return response; 
        }

        [HttpGet("{id}")]
        public async Task<ApiPostResponse<LeaveResponseModel>> GetLeaveRequestById(long id)
        {
            ApiPostResponse<LeaveResponseModel> response = new();
            var result = await _leaveService.GetLeaveRequestById(id);

            if (result != null)
            {
                response.Success = true;
                response.Data = result;
            }
            else
            {
                response.Success = false;
                response.Message = "Leave request not found.";
            }

            return response;
        }

        [HttpGet("/api/leave")]
        public async Task<ApiResponse<LeaveResponseModel>> GetLeaveRequestList([FromQuery] int page = 1, [FromQuery] int pageSize = 5, [FromQuery] string sort = "FromDate", [FromQuery] string dir = "ASC")
        {
            ApiResponse<LeaveResponseModel> response = new() { Data = new List<LeaveResponseModel>()};
            var result = await _leaveService.GetLeaveRequestList(page, pageSize, sort, dir);

            response.Success = true;
            response.Data = result;
            return response;
        }

        [HttpGet("leave-type")]
        public async Task<ApiResponse<LeaveTypeListModel>> GetLeaveTypeList()
        {
            ApiResponse<LeaveTypeListModel> response = new() { Data = new List<LeaveTypeListModel>() };

            var result = await _leaveService.GetLeaveTypeList();

            response.Success = true;
            response.Data = result;
            return response;
        }

        [HttpGet("leave-status")]
        public async Task<ApiResponse<LeaveStatusListModel>> GetLeaveStatusList()
        {
            ApiResponse<LeaveStatusListModel> response = new() { Data = new List<LeaveStatusListModel>() };

            var result = await _leaveService.GetLeaveStatusList();

            response.Success = true;
            response.Data = result;
            return response;
        }
    }
}
