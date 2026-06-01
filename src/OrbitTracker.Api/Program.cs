using Microsoft.EntityFrameworkCore;
using OrbitTracker.Core.BackgroundService;
using OrbitTracker.Core.Contracts;
using OrbitTracker.Core.Service;
using OrbitTracker.Infrastructure.Data;
using OrbitTracker.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrbitTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<SatteliteTrackerWorker>();

builder.Services.AddScoped<ISatelliteTrackingService, SatelliteTrackerService>();
builder.Services.AddScoped<ISatelliteRepository, SatelliteRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

