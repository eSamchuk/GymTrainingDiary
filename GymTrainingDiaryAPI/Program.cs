using GymTrainingDiary.Caching;
using GymTrainingDiary.Utilities.Middleware;
using GymTrainingDiaryAPI.Setup;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Debug()
    .WriteTo.File("D:\\Logs\\Gym\\Log.txt",
        restrictedToMinimumLevel: LogEventLevel.Information,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Hour)
    .CreateLogger();

builder.Host.UseSerilog();

builder.WebHost.ConfigureKestrel(x => x.AllowSynchronousIO = true);


////Caching
var cacheSettings = new CacheSettings();
builder.Configuration.GetSection("RedisCache").Bind(cacheSettings);

if (cacheSettings.IsEnabled)
{
    builder.Services.ConfigureRedisCache(cacheSettings);
}

////AppMetrics
builder.ConfigurationAppMetrics();
////Instances
builder.Services.ConfigureInstances();
////Tracing
builder.Services.ConfigureOpenTelemetryTracing();
////Metrics
builder.Services.ConfigureOpenTelemetryMetrics();
////DbContext
builder.Services.ConfigureDbContext(builder.Configuration);
////Health checks
builder.ConfigureHealthChecks();
////Responce formatting
builder.Services.ConfigureOutputFormatting();


builder.Services.AddSwaggerGen(x =>
{
    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ApiDoc.xml"));
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    x.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = ApiVersion.Default;
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
});

builder.Services.AddEndpointsApiExplorer();
////builder.Host.UseMetricsEndpoints();

var app = builder.Build();

////app.UseEnvInfoEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    //app.UseSwagger(options => { options.RouteTemplate = "api-docs/{documentName}/docs.json"; });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym v1");
        c.RoutePrefix = "api-docs";
    });
}

app.MapHealthChecks("/healthcheck", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

app.UseOpenTelemetryPrometheusScrapingEndpoint();

////app.UseExecutionTimeCalculatorMiddleware();

app.UseHttpLogging();

app.UseSerilogRequestLogging();

app.UseApiExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();