using Cinema.Audit.Data;
using Cinema.Audit.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDapr(
    "cinemaauditconfig",
    new List<string>() { "AuditConnectionString" },
    "cinemaauditsecrets");

builder.ConfigureOpenTelemetry();

builder.Services.AddDbContext<AuditDbContext>(options =>
    options.UseNpgsql(string.Format(
        builder.Configuration["AuditConnectionString"] ?? "{0}{1}",
        builder.Configuration["DbUser"] ?? "",
        builder.Configuration["DbPassword"] ?? ""))
    .UseSnakeCaseNamingConvention());
builder.Services.AddScoped<IAuditRepository, AuditRepository>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuditService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
