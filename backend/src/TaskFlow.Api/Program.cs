using Serilog;
using TaskFlow.Shared.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Frontend");

app.MapGet("/api/health", () =>
{
    var result = new
    {
        status = "Healthy",
        service = "TaskFlow API",
        architecture = "Modular Monolith with Clean Architecture modules",
        timestamp = DateTimeOffset.UtcNow
    };

    return Results.Ok(ApiResponse<object>.Ok(result));
})
.WithName("HealthCheck")
.WithTags("System");

app.Run();