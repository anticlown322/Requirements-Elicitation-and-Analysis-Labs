using PaymentMicroservice.Repository;
using PaymentMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add xml comments
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "PaymentMicroservice.xml");
    options.IncludeXmlComments(xmlPath);
});

//Dependency injection
builder.Services.AddScoped<IPaymentService, DefaultPaymentService>();
builder.Services.AddScoped<IPaymentDbContext, DefaultPaymentDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
