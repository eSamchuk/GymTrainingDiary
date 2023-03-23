using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace GymTrainingDiaryAPI.Setup
{
    public static class OpenTelemetryConfiguration
    {
        public static void ConfigureOpenTelemetryTracing(this IServiceCollection services)
        {
            services.AddOpenTelemetry().WithTracing(opt =>
            {
                opt.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("GymTrainingDiaryAPI"))
                    .AddAspNetCoreInstrumentation()
                    .AddJaegerExporter()
                    .AddZipkinExporter();
            });
        }

        public static void ConfigureOpenTelemetryMetrics(this IServiceCollection services)
        {
            services.AddOpenTelemetry().WithMetrics(metrics =>
            {
                metrics.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("GymTrainingDiaryAPI"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddPrometheusExporter()
                //.AddMeter("ExecutionTimeCalculator")
                //.AddOtlpExporter(opt =>
                //{
                //    opt.Endpoint = new Uri("localhost:4317");
                //})
                .AddConsoleExporter();
            });
        }
    }
}
