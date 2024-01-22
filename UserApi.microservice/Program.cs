using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UserApi.microservice.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbContextUsers>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("conn")));


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
