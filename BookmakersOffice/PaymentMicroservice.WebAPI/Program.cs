//using TransactionMicroservice.Middlewares;

using Kafka.Consumers;
using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Business.Services;
using PaymentMicroservice.Data;
using PaymentMicroservice.Data.Repositories;
using PaymentMicroservice.WebAPI.Middlewares;

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
builder.Services.AddDbContext<TransactionRepositoryContext>(options =>
    options.UseSqlServer(config.GetConnectionString("SqlConnection")));

//custom services
builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddScoped<ITransactionRepository, DefaultTransactionRepository>();
builder.Services.AddScoped<ITransactionService, DefaultTransactionService>();
builder.Services.AddHostedService<TransactionKafkaConsumer>();

//xml documentation
builder.Services.AddSwaggerGen(options => {
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "PaymentMicroservice.WebAPI.xml");
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