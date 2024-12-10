using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


//Add MySQL Db
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"), 
    new MySqlServerVersion(new Version(9, 1, 0))
    ));
//Add Auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
