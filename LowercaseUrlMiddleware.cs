using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

public class LowercaseUrlMiddleware
{
    private readonly RequestDelegate _next;

    public LowercaseUrlMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;

        // Check if the URL contains uppercase letters and if the request is a GET request
        if (request.Method == HttpMethods.Get && request.Path.HasValue && request.Path.Value.Any(char.IsUpper))
        {
            // Convert the URL to lowercase
            var lowerCasePath = request.Path.Value.ToLowerInvariant();
            var queryString = request.QueryString.HasValue ? request.QueryString.Value : string.Empty;

            context.Response.Redirect(lowerCasePath + queryString, true);
            return;
        }

        await _next(context);
    }
}
