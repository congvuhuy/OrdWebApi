using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services.ProductService;
using WebApi.Data.Repository.GroupRepository;
using WebApi.Data.Repository.ProductGroupRepository;
using WebApi.Data.Repository.ProductRepository;
using WebApi.Data.Repository.CommonRepository;
using WebApi.Services.ProductGroupService;
using Microsoft.AspNetCore.Identity;
using WebApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

//Add repo
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();

//Add service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductGroupService, ProductGroupService>();

//Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//Add Jwt Authenticaiton
//builder.Services.AddAuthentication(options => 
//{ 
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
//})
//.AddJwtBearer(options => 
//{ 
//    options.TokenValidationParameters = new TokenValidationParameters
//    { 
//        ValidateIssuer = true, 
//        ValidateAudience = true, 
//        ValidateLifetime = true, 
//        ValidateIssuerSigningKey = true, 
//        ValidIssuer = builder.Configuration["Jwt:Issuer"], 
//        ValidAudience = builder.Configuration["Jwt:Issuer"], 
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) 
//    }; 
//}
//);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try { 
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>(); 
        RoleInitializer.InitializeAsync(roleManager).Wait(); 
    }
    catch (Exception ex)
    { // Handle any errors
      Console.WriteLine(ex.Message); 
    } 
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}



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
