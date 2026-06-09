using Configuration_And_HttpClient.Options;
using Configuration_And_HttpClient.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<FinnHubService>();

// Supply an object of WeatherOptions (with 'weatherapi' section) as Service
builder.Services.Configure<WeatherOptions>(
    builder.Configuration.GetSection("weatherapi")
);



var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

/* reading configuration from appsettings.json

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(
            app.Configuration["MyKey"] + Environment.NewLine);

        await context.Response.WriteAsync(
            app.Configuration.GetValue<string>("MyKey") + Environment.NewLine);

        await context.Response.WriteAsync(
            app.Configuration.GetValue<string>("WrongKey", "Default Value"));
    });
});

*/

app.MapControllers();

app.Run();
