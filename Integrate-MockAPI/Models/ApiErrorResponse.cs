using System;

namespace Integrate_MockAPI.Models;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
        public string Error { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
}
