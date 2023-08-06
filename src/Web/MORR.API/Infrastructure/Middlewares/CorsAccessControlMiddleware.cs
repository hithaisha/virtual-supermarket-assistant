
namespace MORR.API.Infrastructure.Middlewares
{
    public class CorsAccessControlMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        public CorsAccessControlMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(AccessControlAllowOrigin))
                {
                    context.Response.Headers.Add(AccessControlAllowOrigin, "http://localhost:3000");
                }
                return Task.CompletedTask;
            });
            return _next(context);
        }
    }
}
