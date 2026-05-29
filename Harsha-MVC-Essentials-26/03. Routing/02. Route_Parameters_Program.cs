/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Map("files/{filename}.{extension}", async (context) =>
{
    string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
    string? fileExtension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"You're looking for this file: "+ fileName+"."+fileExtension);
});

app.Run();
*/