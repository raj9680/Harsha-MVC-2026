/*

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    string path = context.Request.Path;
    context.Response.Headers["MyKey"] = "My Value";
    context.Response.Headers["Server"] = "My Server";

    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync("<h1>Hello</h1>");
    await context.Response.WriteAsync("<h1>World</h1>");

    // Other Response Headers:
    // Cache-Control
    // Set-Cookie
    // Access-Control-Allow-Origin
    // Location
});


app.Run();

*/