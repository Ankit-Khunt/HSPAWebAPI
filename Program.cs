using HSPAWebAPI.Data;
using HSPAWebAPI.Helpers;
using HSPAWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

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






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseCors(m => m.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
