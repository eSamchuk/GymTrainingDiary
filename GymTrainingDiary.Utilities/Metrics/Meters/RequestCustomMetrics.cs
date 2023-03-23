using System.Diagnostics.Metrics;

namespace GymTrainingDiary.Utilities.Metrics.Meters
{
    public class RequestCustomMetrics
    {
        public Counter<int> RequestsTimedOut { get; }

        public string MetricName { get; }

        public RequestCustomMetrics(string meterName = "MyMeter") //nameof(RequestCustomMetrics)
        {
            var meter = new Meter(meterName);
            this.MetricName = meterName;
            this.RequestsTimedOut = meter.CreateCounter<int>("timed-out", "request");
        }

        public void AddTimedOutRequest() => this.RequestsTimedOut.Add(1);

    }
}
