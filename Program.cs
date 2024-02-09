using HSPAWebAPI.Data;
using HSPAWebAPI.Helpers;
using HSPAWebAPI.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Extensions;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// for not getting cors error to use send data on different urls
builder.Services.AddCors();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));

// Register repo if we created
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Add Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

// Add JWT token authentication
var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
var key = new SymmetricSecurityKey(Encoding.UTF8
    .GetBytes(secretKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                // services.AddAuthentication("Bearer")
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key
                    };
                });





var app = builder.Build();
app.ConfigureExceptionHandler(app.Environment);
app.ConfigureBuiltinExceptionHandler(app.Environment);
app.UseMiddleware<ExceptionMiddleware>();


// use cors (allow other port number to access)
app.UseCors(m => m.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// use for jwt authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
