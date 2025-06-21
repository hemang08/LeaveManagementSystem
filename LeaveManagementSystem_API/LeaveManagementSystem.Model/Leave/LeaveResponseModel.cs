using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Model.Leave
{
    public class LeaveResponseModel
    {
        public long? LeaveRequestId { get; set; }
        public required string EmployeeName { get; set; }
        public string? LeaveType { get; set; }
        public long? LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Status { get; set; }
        public long? StatusId { get; set; }
        public long? TotalCount { get; set; }
    }

    public class LeaveTypeListModel
    {
        public int LeaveTypeId { get; set; }
        public string? Name { get; set; }
    }
    public class LeaveStatusListModel
    {
        public int LeaveStatusId { get; set; }
        public string? Name { get; set; }
    }
}
