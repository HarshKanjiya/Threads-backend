using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Ocelot.json");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerForOcelotUI(builder.Configuration);

}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("Ocelot.json", "Ocelot");
});

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials());
app.UseOcelot().Wait();
app.UseHttpsRedirection();

//app.UseCors(o => o.AllowAnyHeader().WithOrigins("http://localhost:4200/"));


app.UseAuthorization();

app.MapControllers();


app.Run();
