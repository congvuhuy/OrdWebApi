using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services.ProductService;
using WebApi.Data.Repository.GroupRepository;
using WebApi.Data.Repository.ProductGroupRepository;
using WebApi.Data.Repository.ProductRepository;
using WebApi.Data.Repository.CommonRepository;
using WebApi.Services.ProductGroupService;

var builder = WebApplication.CreateBuilder(args);

// 
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
//Add service
//Add repo
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductGroupService, ProductGroupService>();


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
