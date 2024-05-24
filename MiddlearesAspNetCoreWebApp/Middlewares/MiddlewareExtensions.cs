namespace MiddlearesAspNetCoreWebApp.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddTimingMiddleware(this WebApplication webApplication)
        {
            return webApplication.UseMiddleware<TimingMiddleware>();
        }
    }
}
