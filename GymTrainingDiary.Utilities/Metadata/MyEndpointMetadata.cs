using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymTrainingDiary.Utilities.Metadata
{
    public class MyEndpointMetadata
    {
        public string ControllerMethod { get; set; }

        public string Action { get; set; }

        public bool IsObsolete { get; set; }

        public string DisplayName { get; set; }

        public string RoutePattern { get; set; }

        public string ApiVersion { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public bool IsAnonAllowed { get; set; }

        [JsonIgnore()]
        public List<ActionParameter> Parameters { get; set; } = new();

        [JsonPropertyName("Parameters")]
        public List<ActionParameter>? ParametersToSee => this.Parameters.Any() ? this.Parameters : null;





    }
}
