/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Map("/", async (context) =>
{
    await context.Response.WriteAsync("On Home Page");
});


app.MapGet("map1", async (context) =>
{
    await context.Response.WriteAsync("In Map 1");
});


app.MapPost("map2", async (context) =>
{
    await context.Response.WriteAsync("In Map 2");
});


app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync("Not Found");
});

app.Run();
*/