/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMyCustomMiddleware();

app.Run();



#region Custom Class

public class MyCustomMiddlewareClass
{
    private readonly RequestDelegate _next;
    public MyCustomMiddlewareClass(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await context.Response.WriteAsync("Hello Before Logic\n"); // before logic
        await _next(context);
        await context.Response.WriteAsync("Hello After Logic"); // after logic
    }
}


// Extension Method used to add the Middleware
public static class MyCustomMiddlewareExtentions
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<MyCustomMiddlewareClass>();
    }
}

#endregion
*/