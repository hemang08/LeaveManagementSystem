namespace LeaveManagementSystem.Common.Helpers
{
    public class StoredProcedures
    {
        #region Leave
        public const string DeleteLeaveRequest = "sp_DeleteLeaveRequest";
        public const string GetLeaveRequestById = "sp_GetLeaveRequestById";
        public const string GetLeaveRequestList = "sp_GetLeaveRequestList";
        public const string SaveLeaveRequest = "sp_SaveLeaveRequest";
        public const string GetLeaveType = "SP_GetLeaveType";
        public const string GetLeaveStatus = "SP_GetLeaveStatus";
        #endregion
    }
}