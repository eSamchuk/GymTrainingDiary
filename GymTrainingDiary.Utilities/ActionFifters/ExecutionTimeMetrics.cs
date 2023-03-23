using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace GymTrainingDiary.Utilities.ActionFifters
{
    public class ExecutionTimeMetrics : IAsyncActionFilter
    {
        private static readonly Meter _metric = new Meter("MyMeter");
        private readonly Histogram<long> executionTimeHistogram;

        public ExecutionTimeMetrics()
        {
            this.executionTimeHistogram = _metric.CreateHistogram<long>("exexution_time", "ms");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await next();
            sw.Stop();

            this.executionTimeHistogram.Record(sw.ElapsedMilliseconds);
        }
    }
}
