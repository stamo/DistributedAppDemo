using Cinema.HallManager.Data;
using Cinema.HallManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDapr(
    "cinemahallsconfig", 
    new List<string>() { "HallsConnectionString" }, 
    "cinemahallssecrets");

builder.ConfigureOpenTelemetry();

builder.Services.AddDbContext<HallsDbContext>(options =>
    options.UseNpgsql(string.Format(
        builder.Configuration["HallsConnectionString"] ?? "{0}{1}",
        builder.Configuration["DbUser"] ?? "",
        builder.Configuration["DbPassword"] ?? ""))
    .UseSnakeCaseNamingConvention());
builder.Services.AddScoped<IHallsRepository, HallsRepository>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<HallsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
