using Microsoft.EntityFrameworkCore;
using OrbitTracker.Core.BackgroundService;
using OrbitTracker.Core.Contracts;
using OrbitTracker.Core.Service;
using OrbitTracker.Infrastructure.Data;
using OrbitTracker.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Swagger конфигурация
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "OrbitTracker API", 
        Version = "v1" 
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<OrbitTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISatelliteRepository, SatelliteRepository>();

builder.Services.AddHttpClient<ISatelliteTrackingService, SatelliteTrackerService>(client =>
{
    client.BaseAddress = new Uri("http://api.open-notify.org/");
});

builder.Services.AddHostedService<SatteliteTrackerWorker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrbitTracker API v1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();