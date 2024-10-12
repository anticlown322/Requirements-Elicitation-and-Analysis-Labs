//using UserMicroservice.Middlewares;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Business.Services;
using UserMicroservice.Data;
using UserMicroservice.Data.Repositories;
using UserMicroservice.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

/* Add services to the container. */
//basic
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//database
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("SqlConnection")));

//custom services
builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddScoped<IUserRepository, DefaultUserRepository>();
builder.Services.AddScoped<IUserService, DefaultUserService>();

//xml documentation
builder.Services.AddSwaggerGen(options => {
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "UserMicroservice.WebAPI.xml");
    options.IncludeXmlComments(xmlPath);
});

/* Configure the HTTP request pipeline. */
var app = builder.Build();

app.UseExceptionHandlerMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();