using UserMicroservice.Repository;
using UserMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add xml comments
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "UserMicroservice.xml");
    options.IncludeXmlComments(xmlPath);
});

//Dependency injection
builder.Services.AddScoped<IUserService, DefaultUserService>();
builder.Services.AddScoped<IUserDbContext, DefaultUsersDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();