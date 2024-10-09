//using UserMicroservice.Middlewares;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Business.Services;
using UserMicroservice.Data;
using UserMicroservice.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

/* Add services to the container. */
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserDB;Trusted_Connection=True;"));
builder.Services.AddScoped<IUserRepository, DefaultUserRepository>();
builder.Services.AddScoped<IUserService, DefaultUserService>();

builder.Services.AddSwaggerGen(options => {
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "UserMicroservice.WebAPI.xml");
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

/* Configure the HTTP request pipeline. */
//app.UseExceptionHandlerMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.MapControllers();

app.Run();