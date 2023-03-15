using GymTrainingDiary.Data;
using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.DataAccess.Repositories;
using GymTrainingDiary.Utilities.Middleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

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

builder.Services
    .AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("GymConnectionString"), name: "Gym")
    .AddSqlServer(builder.Configuration.GetConnectionString("RecipesDb"), name: "Recipes");

builder.Services
    .AddControllers(opt =>
    {
        opt.RespectBrowserAcceptHeader = true;
    })
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.WriteIndented = false;
        opt.JsonSerializerOptions.AllowTrailingCommas = false;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        opt.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        opt.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
        opt.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
    })
    .AddXmlSerializerFormatters();

builder.Services.AddDbContext<GymTrainingDataContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("GymConnectionString"));
}, ServiceLifetime.Scoped);

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration["RedisCache:Endpoint"];
});

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
    //x.UnsupportedApiVersionStatusCode = 400;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();
builder.Services.AddTransient<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IWorkoutRepository, WorkoutsRepository>();
builder.Services.AddTransient<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
builder.Services.AddSingleton(Log.Logger);
builder.Services.AddControllers();

//builder.Services.ConfigureOptions<SwaggerUIConfiguration>();
var app = builder.Build();


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

app.UseHttpLogging();

app.UseSerilogRequestLogging();

app.UseApiExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();