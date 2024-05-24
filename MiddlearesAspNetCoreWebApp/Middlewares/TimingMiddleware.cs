namespace MiddlearesAspNetCoreWebApp.Middlewares
{
    public class TimingMiddleware
    {
        private readonly ILogger<TimingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public TimingMiddleware(ILogger<TimingMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var startTime = DateTime.UtcNow;
            await _next.Invoke(context);
            _logger.LogInformation($"Timing called from  Path: {context.Request.Path} | At: {DateTime.UtcNow - startTime}");
        }
    }
}
