var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var countries = new Dictionary<int, string>
{
    { 1, "United States" },
    { 2, "Canada" },
    { 3, "United Kingdom" },
    { 4, "India" },
    { 5, "Japan" }
};

app.MapGet("/countries", async(HttpContext context) =>
{
    foreach(var country in countries)
    {
        await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
    }
});

app.MapGet("/countries/{countryId:int:range(1, 100)}", async (HttpContext context) =>
{
    if(!context.Request.RouteValues.ContainsKey("countryId"))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("No country id found");
    }

    int countryId = Convert.ToInt32(context.Request.RouteValues["countryId"]);

    if(countries.ContainsKey(countryId))
    {
        string countryName = countries[countryId];

        await context.Response.WriteAsync($"{countryName}");
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("No country match with this id.");
    }
});

app.MapGet("/countries/{countryId:min(101)}", async (HttpContext context) =>
{
    context.Response.StatusCode = 400;
    await context.Response.WriteAsync($"The CountryID should be between 1 and 100.");
});

// test commit from another machine ssh

app.Run();
