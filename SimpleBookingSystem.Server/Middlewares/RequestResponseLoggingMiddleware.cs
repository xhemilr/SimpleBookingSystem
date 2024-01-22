using System.Text;

namespace SimpleBookingSystem.Server.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log the incoming request
            LogRequest(context.Request);


            // Call the next middleware in the pipeline
            await _next(context);

            // Log the outgoing response
            LogResponse(context.Response);
        }

        private void LogRequest(HttpRequest request)
        {
            _logger.LogInformation($"Request received: {request.Method} {request.Path}, Host: {request.Host}");
        }

        private void LogResponse(HttpResponse response)
        {
            _logger.LogInformation($"Response sent: {response.StatusCode}");
        }
    }
}
