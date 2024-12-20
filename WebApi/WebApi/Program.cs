﻿using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services.ProductService;
using WebApi.Data.Repository.ProductGroupRepositoryFolder;
using WebApi.Data.Repository.ProductRepositoryFolder;
using WebApi.Data.Repository.CommonRepository;
using WebApi.Services.ProductGroupService;
using Microsoft.AspNetCore.Identity;
using WebApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Services.UserService;
using WebApi.Data.Repository.UserRepository;
using WebApi.Helpers;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using WebApi.Data.Repository.UnitOfWorkFolder;

var builder = WebApplication.CreateBuilder(args);

// 
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
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
builder.Services.AddSwaggerGen(c => { 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); 
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { 
        In = ParameterLocation.Header, 
        Description = "Please enter token", 
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey, 
        BearerFormat = "JWT", 
        Scheme = "Bearer" 
    }); 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
    { 
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                } 
            }, new string[] { } 
        } 
    }); 
});

//Add repo
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Add service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductGroupService, ProductGroupService>();
builder.Services.AddScoped<IUserService, UserService>();

//Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//Add Jwt Authenticaiton
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
}
);
var app = builder.Build();


//add role and Admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try { 
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>(); 
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        SeedRoleAndAdmin.InitializeAsync(roleManager,userManager).Wait(); 
    }
    catch (Exception ex)
    { 
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

app.UseAuthentication();//Midleware xác Jwt token
app.UseAuthorization();//Midleware xác thực quyền

app.MapControllers();

app.Run();
