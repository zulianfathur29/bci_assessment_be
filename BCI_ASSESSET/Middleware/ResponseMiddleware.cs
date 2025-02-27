using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BCI_ASSESSET.Response;


namespace BCI_ASSESSET.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Store the original response stream
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body = originalBodyStream;
                var responseContent = await FormatResponse(context.Response);

                await context.Response.WriteAsync(responseContent);
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            var apiResponse = new ResultResponse<object>(
                success: response.StatusCode >= 200 && response.StatusCode < 300,
                message: response.StatusCode >= 200 && response.StatusCode < 300 ? "Success" : "Error",
                data: null
            );

            response.ContentType = "application/json";
            return JsonSerializer.Serialize(apiResponse);
        }
    }
}
