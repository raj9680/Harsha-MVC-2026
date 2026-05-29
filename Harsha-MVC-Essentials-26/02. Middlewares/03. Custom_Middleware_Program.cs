/*
var builder = WebApplication.CreateBuilder(args);

// register the custom-middleware here // 2
builder.Services.AddTransient<MyCustomMiddlewareClass>();

var app = builder.Build();


app.Use(async (HttpContext context, RequestDelegate next) => 
{
    await context.Response.WriteAsync("Hello Default Middleware\n\n");
    await next(context);
});


app.UseMiddleware<MyCustomMiddlewareClass>(); // 3

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Default Middleware 3");
});

app.Run();



// create custom middleware -- 1
#region Custom Class

public class MyCustomMiddlewareClass : IMiddleware
{
    // overloaded from Interface
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("My Custom Middleware - Starts\n");
        await next(context);

        await context.Response.WriteAsync("\nMy Custom Middleware - Ends\n");
    }
}


#endregion
*/