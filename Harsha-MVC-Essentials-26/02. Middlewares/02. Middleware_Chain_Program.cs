/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello\n");
    await next(context);
});

// Middleware2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello Again\n");
    await next(context);
});

// Middleware3 - app.Run() is short circuiting middleware
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello Again Again");
});

app.Run();
*/