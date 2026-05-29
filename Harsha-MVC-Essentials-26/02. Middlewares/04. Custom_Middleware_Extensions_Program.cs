/*

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddlewareClass>();
var app = builder.Build();


app.UseMyCustomMiddleware(); // 3
app.Run();


// CustomMiddleware
#region Custom Class

public class MyCustomMiddlewareClass : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("My Custom Middleware - Starts\n");
        await next(context);

        await context.Response.WriteAsync("\nMy Custom Middleware - Ends\n");
    }
}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MyCustomMiddlewareClass>();
    }
}


#endregion

*/