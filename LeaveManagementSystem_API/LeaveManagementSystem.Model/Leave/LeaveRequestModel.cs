namespace LeaveManagementSystem.Model.Leave
{
    public class LeaveRequestModel : LeaveModel
    {
        public long? LeaveRequestId { get; set; }

    }

    public class LeaveModel
    {
        public required string EmployeeName { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int StatusId { get; set; }
    }
}
