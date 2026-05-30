using OrbitTracker.Core.BackgroundService;
using OrbitTracker.Core.Contracts;
using OrbitTracker.Infrastructure.Data;
using OrbitTracker.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add PostgreSQL DbContext
builder.Services.AddDbContext<OrbitTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repository
builder.Services.AddScoped<ISatelliteRepository, SatelliteRepository>();

var app = builder.Build();

builder.Services.AddHostedService<SatteliteTrackerWorker>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();

