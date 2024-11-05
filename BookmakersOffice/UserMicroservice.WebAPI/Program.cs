using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Kafka.Interfaces;
using Kafka.Producers;
using UserMicroservice.Business.Services;
using UserMicroservice.Data;
using UserMicroservice.Data.Repositories;

using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

//using UserMicroservice.WebAPI.Middlewares;

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
builder.Services.AddDbContext<UserRepositoryContext>(options =>
    options.UseSqlServer(config.GetConnectionString("SqlConnection")));

//custom services
//builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddScoped<IUserRepository, DefaultUserRepository>();
builder.Services.AddScoped<IUserService, DefaultUserService>();
builder.Services.AddScoped<IKafkaProducer, UserKafkaProducer>();

//xml documentation
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "UserMicroservice.WebAPI.xml");
    options.IncludeXmlComments(xmlPath);
});

//logger
configureLogging();
builder.Host.UseSerilog();

/* Configure the HTTP request pipeline. */
var app = builder.Build();

//app.UseExceptionHandlerMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

void configureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true).Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string enviroment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfig:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{enviroment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
        NumberOfReplicas = 1,
        NumberOfShards = 2
    };
}