using EmployeeDirectory.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=employees.db"));

builder.Services.AddControllers();

// 🔥 ADD SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔥 ENABLE SWAGGER
app.UseSwagger();
app.UseSwaggerUI();

// Create DB automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Enable UI
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run("http://0.0.0.0:5000");