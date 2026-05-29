/*
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("products/details/{id:int?}", async (context) =>
{
    int id = Convert.ToInt32(context.Request.RouteValues["id"]);
    await context.Response.WriteAsync($"{id}");
});

// /daily-digest-report/2020-06-01
app.Map("daily-digest-report/{reportdate:datetime}", async (context) =>
{
    DateTime date = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
    await context.Response.WriteAsync($"Date is: {date.Date}");
});

app.Run();


Note: We have more types of route constraints-
1. We can use min:max length values in constraints.
2. We can use regex in constrains. etc.. 
*/