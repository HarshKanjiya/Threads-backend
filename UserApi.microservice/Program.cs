using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using UserApi.microservice.Data;
using UserApi.microservice.Models;
using UserApi.microservice.services;
using UserAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMessageProducer, MessageProducer>();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DbContextUsers>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddSingleton<JwtTokenHandler>();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 52428800; // Set the limit to 50 MB (in bytes)
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(o => o.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin());

app.Run();
