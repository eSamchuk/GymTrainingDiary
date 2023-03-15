using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GymTrainingDiaryAPI.Configuration
{

    /// <summary>
    /// SwaggerUI Configuration
    /// </summary>
    public class SwaggerUIConfiguration : IConfigureNamedOptions<SwaggerUIOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public SwaggerUIConfiguration(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(string? name, SwaggerUIOptions options)
        {
            this.Configure(options);
        }

        /// <summary>
        /// Configures SwaggerUI Options
        /// </summary>
        /// <param name="options">SwaggerUI Options</param>
        public void Configure(SwaggerUIOptions options)
        {
            options.RoutePrefix = "api-docs";
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"{description.GroupName}/docs.json", description.GroupName);
            }
        }
    }
}
