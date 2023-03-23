using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace GymTrainingDiary.Utilities.Middleware
{
    public class ExecutionTimeCalculatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Meter _requestExecutionTimeMeter = new Meter("ExecutionTimeCalculator");
        private readonly Histogram<long> _histogram;

        public ExecutionTimeCalculatorMiddleware(RequestDelegate next)
        {
            this._next = next;
            this._histogram = this._requestExecutionTimeMeter.CreateHistogram<long>("execution_time", "ms", "Execution time of individual request");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Stopwatch sw = Stopwatch.StartNew();

            await _next(httpContext);

            sw.Stop();
            this._histogram.Record(sw.ElapsedMilliseconds);

        }
    }

    public static class MiddlewareExtensions
    { 
        public static IApplicationBuilder UseExecutionTimeCalculatorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExecutionTimeCalculatorMiddleware>();
        }
    }
}
