//using App.Metrics.Formatters.Ascii;
//using App.Metrics.Formatters.Prometheus;

namespace GymTrainingDiaryAPI.Setup
{
    public static class AppMetricsConfiguration
    {
        public static void ConfigurationAppMetrics(this WebApplicationBuilder builder)
        {
            //builder.Services.AddMetrics();
            //builder.Host.UseMetricsWebTracking();
            //builder.Host.UseMetricsEndpoints(x =>
            //{
            //    x.MetricsTextEndpointOutputFormatter = new MetricsTextOutputFormatter();
            //    x.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            //    x.EnvironmentInfoEndpointEnabled = true;
            //    x.MetricsEndpointEnabled = false;
            //});

            //builder.Services.AddMetricsEndpoints(x =>
            //{
            //    x.MetricsTextEndpointOutputFormatter = new MetricsTextOutputFormatter();
            //    x.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            //    x.EnvironmentInfoEndpointEnabled = true;
            //    x.MetricsEndpointEnabled = false;
            //});

        }
    }
}
