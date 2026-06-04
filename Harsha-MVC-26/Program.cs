
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//IOC Container

//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICitiesService),
//    typeof(CitiesService),
//    ServiceLifetime.Transient
//));

// Short Way
builder.Services.AddTransient<ICitiesService, CitiesService>();

// ends

builder.Services.AddTransient<CitiesService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

