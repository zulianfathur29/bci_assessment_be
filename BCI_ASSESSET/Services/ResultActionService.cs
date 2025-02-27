using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BCI_ASSESSET.Services
{
    public class ResultActionService : IActionResult
    {
        private readonly bool _success;
        private readonly string _message;
        private readonly object? _payload;

        public ResultActionService(bool success, string message, object? payload)
        {
            _success = success;
            _payload = payload;
            _message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";

            var result = new
            {
                Success = (response.StatusCode >= 200 && response.StatusCode < 300 && _success == true) ? true : false,
                Message = _message,
                Payload = _payload
            };

            await response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
