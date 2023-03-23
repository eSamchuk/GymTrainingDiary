using GymTrainingDiary.Caching.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Caching.Filter
{
    public class CachingFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var settings = context.HttpContext.RequestServices.GetRequiredService<CacheSettings>();

            if (!settings.IsEnabled)
            {
                await next();
                return;
            }

            var service = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var key = GenerateKeyFromRequest(context.HttpContext.Request);

            var response = await service.ReadFromCacheAsync(key);

            if (!string.IsNullOrEmpty(response))
            {
                var contentResult = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                context.Result = contentResult;
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult result)
            {
                ////TODO
                ////await service.PutInCacheAsync(key, result.Value, TimeSpan.FromSeconds(this.timeToLive));
            }
        }

        private string GenerateKeyFromRequest(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(request.Path);

            var parameters = request.Query.OrderBy(x => x.Key);

            foreach (var (key, value) in parameters)
            {
                sb.Append($"|{key}-{value}");
            }

            return sb.ToString();
        }
    }
}
