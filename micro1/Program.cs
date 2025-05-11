using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CSharpService.Models;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Настройка Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "C# Microservice API", 
        Version = "v1",
        Description = "API for C# Microservice with PostgreSQL"
    });
});

// Настройка БД
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Конфигурация middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "C# Microservice v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Инициализация БД
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();