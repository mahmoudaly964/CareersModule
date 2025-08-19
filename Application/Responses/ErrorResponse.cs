using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; } = null;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
