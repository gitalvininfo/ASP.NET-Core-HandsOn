using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace _04_Middlewares.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            if(httpContext.Request.Query.ContainsKey("firstName") && httpContext.Request.Query.ContainsKey("lastName"))
            {

                string? firstName = httpContext.Request.Query["firstName"];
                string? lastName = httpContext.Request.Query["lastName"];

                await httpContext.Response.WriteAsync($"Hello {firstName} {lastName}\n");
            }

            await _next(httpContext);

            await httpContext.Response.WriteAsync($"End of Hello \n");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ConventionalMiddlewareExtensions
    {
        public static IApplicationBuilder UseConventionalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConventionalMiddleware>();
        }
    }
}
