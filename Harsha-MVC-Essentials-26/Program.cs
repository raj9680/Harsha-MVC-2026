using harsha_mvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Step 1 ---- adds all controllers classes as services
builder.Services.AddControllersWithViews(options=>
{
    // added PersonModelProvider as reusable class wherever Person model find
    // disabled custom model binder temp. 
    // options.ModelBinderProviders.Insert(0, new PersonBinderProvider());
}).AddXmlSerializerFormatters(); 

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
// Step 2 ---- enabled routing for all controllers methods
app.MapControllers();       

app.Run();