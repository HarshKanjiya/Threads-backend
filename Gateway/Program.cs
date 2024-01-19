using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();
app.MapControllers();

app.Run();
