using Azure.Core.Pipeline;

namespace Test_Web_API.Models
{
    public class ApiResult
    {
        public object? Data { get; set; }

        public string? ErrorMessage { get; set; }

        public bool? Success { get; set; }

        public string? Message { get; set; }
    }
}
