using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Application.Responses
{
    public class SuccessResponse<T> : SuccessResponse
    {
        public T? Result { get; set; }
        public int StatusCode { get; set; } = 200;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool isSuccess { get; set; } = true;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public SuccessResponse()
        {
        }

        public SuccessResponse(T result, int statusCode = 200)
        {
            Result = result;
            StatusCode = statusCode;
        }
    }
}