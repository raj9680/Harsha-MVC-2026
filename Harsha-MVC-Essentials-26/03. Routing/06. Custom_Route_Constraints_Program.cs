/*

using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Register Constraints in Router
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraints));
});

var app = builder.Build();

app.Map("sales-report/{year:int:min(1900)}/{month:months}", async (context) =>
{
    int year = Convert.ToInt32(context.Request.RouteValues["year"]);
    string? month = Convert.ToString(context.Request.RouteValues["month"]);

    if(month == "apr" || month == "jul" || month=="oct" || month == "jan")
    {
        await context.Response.WriteAsync($"sales report - {year} - {month}");
    }
    else
    {
        await context.Response.WriteAsync($"{month} is not allowed for sales report");
    }
});


app.Run();


#region CustomConstraintsClass

// Eg: sales-report/2040/apr
public class MonthsCustomConstraints : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey)) // month
        {
            return false; // not a match
        }

        Regex regex = new Regex($"^(apr|jul|oct|jan)$");
        string? monthValue = Convert.ToString(values[routeKey]);

        if(regex.IsMatch(monthValue))
        {
            return true; // its a match
        }

        return false; // not a match
    }
}

#endregion


*/