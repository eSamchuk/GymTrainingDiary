using GymTrainingDiary.Utilities.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace GymTrainingDiaryAPI.Controllers.v1
{
    [Area("api")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly EndpointDataSource datasource;

        public InfoController(EndpointDataSource datasource)
        {
            this.datasource = datasource;
        }

        [HttpGet()]
        [MapToApiVersion("1.0")]
        public ActionResult GetAllRoutes()
        {
            var dict = new Dictionary<string, List<MyEndpointMetadata>>();
            var endpoints = datasource.Endpoints.Cast<RouteEndpoint>();

            foreach (var e in endpoints)
            {
                var controller = e.Metadata.OfType<ControllerActionDescriptor>()
                                          .FirstOrDefault();

                var action = controller != null ? $"{controller.ControllerName}.{controller.ActionName}" : null;

                var controllerMethod = controller != null ? $"{controller.MethodInfo.Name}" : null;

                var controllerApiversions = controller.ControllerTypeInfo.GetCustomAttributes<ApiVersionAttribute>();

                var methodApiVersions = controller.MethodInfo.GetCustomAttributes<MapToApiVersionAttribute>();

                if (!dict.ContainsKey(controller.ControllerName))
                {
                    dict.Add(controller.ControllerName, new List<MyEndpointMetadata>());
                }

                dict[controller.ControllerName].Add(new MyEndpointMetadata
                {
                    Action = action,
                    RoutePattern = e.RoutePattern?.RawText,
                    IsAnonAllowed = e.Metadata.GetMetadata<IAllowAnonymous>() != null,
                    ControllerMethod = controllerMethod,
                    DisplayName = e.DisplayName,
                    IsObsolete = controller.MethodInfo.GetCustomAttribute<ObsoleteAttribute>() != null,
                    ApiVersion = methodApiVersions != null ? string.Join(", ", methodApiVersions.SelectMany(x => x.Versions).Select(x => $"{x.MajorVersion}.{x.MinorVersion}")) : "default",
                    Parameters = controller.MethodInfo.GetParameters()
                       .Select(x => new ActionParameter
                       {
                           Name = x.Name,
                           Source = this.GetParameterSource(x),
                           Type = x.ParameterType.Name,
                           Position = x.Position + 1,
                           IsOptional = x.IsOptional,
                           DefaultValue = x.DefaultValue
                       }).ToList()
                });
            }

            return Ok(dict.Select(x => new MyControllerMetadata { ControllerName = x.Key, EndpointsInfo = x.Value }));
        }

        private string GetParameterSource(ParameterInfo x)
        {
            string result = "FromQuery";
            var attributes = x.GetCustomAttributes(false);

            if (attributes.Any())
            {
                var attributeTypeName = attributes.First().GetType().Name;
                result = attributeTypeName.Remove(attributeTypeName.IndexOf("Attribute"));
            }

            return result;
        }
    }
}