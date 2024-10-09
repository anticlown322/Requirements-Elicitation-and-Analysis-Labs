using Microsoft.EntityFrameworkCore;
using UserMicroservice.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DefaultUserContext>(opt =>
    opt.UseInMemoryDatabase("UserList"));
builder.Services.AddEndpointsApiExplorer();

//add xml comments
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;

    var xmlPath = Path.Combine(basePath, "UserMicroservice.WebAPI.xml");
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();