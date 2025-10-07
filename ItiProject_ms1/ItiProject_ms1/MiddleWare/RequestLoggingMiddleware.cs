namespace ItiProject_ms1.MiddleWare
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "requests_log.txt");

            // Ensure the Logs directory exists
            if (!Directory.Exists(Path.GetDirectoryName(_logFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath));
            }
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"Path is {context.Request.Path} | User is {(context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "Anonymous")} | Timestamp is {DateTime.Now}");

            await _next(context);
        }
    }
}
