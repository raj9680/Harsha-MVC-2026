/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Map("employee/profile/{EmployeeName?}", async (context) => // ? Optional
{
    string? empName = Convert.ToString(context.Request.RouteValues["employeename"]);
    if (context.Request.RouteValues.ContainsKey("employeename"))
    {
        await context.Response.WriteAsync($"Employee Name is: {empName}");
    }
    else
    {
        await context.Response.WriteAsync("Parameter not found");
    }
    
});


app.Run();
*/