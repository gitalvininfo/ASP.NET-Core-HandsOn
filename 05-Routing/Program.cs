var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//enable routing
app.UseRouting();


app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();

    if(endpoint != null)
    {
        await context.Response.WriteAsync($"Endpoint: {endpoint.DisplayName}");
    }

    await next(context);
});

// creating endpoints
app.UseEndpoints(endpoints =>
{

    // add your endpoints
    endpoints.MapGet("/me", async (context) =>
    {
        await context.Response.WriteAsync($"Hello Me Path!: {context.Request.Path}");
    });

    endpoints.MapGet("/hello", async (context) =>
    {
        await context.Response.WriteAsync($"Hello World Path!: {context.Request.Path}");
    });


    endpoints.MapGet("files/{fileName}.{extension}", async (context) =>
    {

        string? fileName = Convert.ToString(context.Request.RouteValues["fileName"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);

        await context.Response.WriteAsync($"In Files - {fileName}.{extension}");
    });

    endpoints.MapGet("employee/profile/{name:minlength(3):maxlength(10)}", async (context) =>
    {

        string? name = Convert.ToString(context.Request.RouteValues["name"]);

        await context.Response.WriteAsync($"In Employee - {name}");
    });

    endpoints.MapGet("product/details/{id?}", async (context) =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int id = Convert.ToInt32(context.Request.RouteValues["id"]);

            await context.Response.WriteAsync($"Product Details: {id}");
        }
        else
        {
            await context.Response.WriteAsync($"Product Details Id not supplied");
        }
    });
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync($"Hello Default Path!: {context.Request.Path}");
});

app.Run();