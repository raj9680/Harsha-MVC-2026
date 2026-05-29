/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Map("employee/profile/{EmployeeName=scott}", async (context) =>
{
    string? empName = Convert.ToString(context.Request.RouteValues["employeename"]);
    await context.Response.WriteAsync($"Employee Name is: {empName}");
});


app.Run();
*/