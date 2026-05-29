/*

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// HttpContext automatically gets created when browser sends request & context contains
// info. related to request, response and many more details

app.Run(async (HttpContext context) =>
{
    context.Response.StatusCode = 400; // for passing status code
    await context.Response.WriteAsync("Hello World"); // for writing in response Body

    //async - await means only after the completion of that statement further code will executes
});


app.Run();


*/