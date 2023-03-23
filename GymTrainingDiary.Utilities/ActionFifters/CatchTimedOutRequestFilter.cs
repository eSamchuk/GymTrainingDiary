using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Metrics;

namespace GymTrainingDiary.Utilities.ActionFifters
{
    public class CatchTimedOutRequestFilter : IAsyncActionFilter
    {
        private static readonly Meter _metric = new Meter("MyMeter");
        private readonly Counter<int> timedOutRequestsCounter;

        public CatchTimedOutRequestFilter()
        {
            this.timedOutRequestsCounter = _metric.CreateCounter<int>("timed-out", "request", "Number of timed out requests");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            //if (context.HttpContext.Response.StatusCode == StatusCodes.Status408RequestTimeout)
            {
                this.timedOutRequestsCounter.Add(1);
                var t = this.timedOutRequestsCounter.Enabled;
            }
        }
    }
}
