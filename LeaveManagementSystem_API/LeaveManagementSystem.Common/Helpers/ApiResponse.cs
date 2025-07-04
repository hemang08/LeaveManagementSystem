﻿using System.Text.Json;

namespace LeaveManagementSystem.Common.Helpers
{
    public class ApiResponse<T> : BaseApiResponse
    {
        public virtual IList<T> Data { get; set; }
    }
    public class ApiPostResponse<T> : BaseApiResponse
    {
        public virtual T Data { get; set; }
    }
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {

        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class Response : BaseApiResponse
    {
        public long TAID { get; set; }
    }
}
