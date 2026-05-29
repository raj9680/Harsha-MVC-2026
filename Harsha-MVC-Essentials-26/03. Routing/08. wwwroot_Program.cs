/*

using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "assets"
});

var app = builder.Build();

app.UseStaticFiles();   // works well with default & assets (is a folder) folder

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(builder.Environment.ContentRootPath + @"\myroot")
}); // works with multiple folders - myroot is a folder


app.Map("/", async (context) =>
{
    await context.Response.WriteAsync("On Home Page");
});

app.Run();

*/