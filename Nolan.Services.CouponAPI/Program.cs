using Microsoft.EntityFrameworkCore;
using Nolan.Services.CouponAPI.Data;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration
// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

#region Pipeline Configuration
// Configure the HTTP request pipeline.

// If the application is in development mode, enable Swagger and Swagger UI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS.

app.UseAuthorization(); // Apply authorization middleware.

app.MapControllers(); // Map controller routes.

ApplyMigration(); // Apply database migrations.

app.Run(); // Start the application.

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (db.Database.GetPendingMigrations().Any())
        {
            db.Database.Migrate();
        }
    }
}
#endregion
