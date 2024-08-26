using _04_Middlewares.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CustomMiddleware>();

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Start of Middleware 1!\n");

    await next(context);


    await context.Response.WriteAsync("End of Middleware 1!\n");
});

// Old style of using middleware
//app.UseMiddleware<CustomMiddleware>();

//app.UseMyCustomMiddleware();



// Newer style of using middleware starting .NET6
app.UseConventionalMiddleware();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Start of Middleware 3!\n");

    await next(context);

    await context.Response.WriteAsync("End of Middleware 3!\n");
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Short circuit the middleware here!\n");
});


app.Run();
