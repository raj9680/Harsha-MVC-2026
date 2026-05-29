/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// useWhen - acts as a conditional middleware
app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
    app =>
    {
        app.Use(async (context, next) =>
        {
            string? username = context.Request.Query["username"];
            await context.Response.WriteAsync($"username is: {username}\n");
            await next(context);
        });
   });


app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello");
});

app.Run();

*/