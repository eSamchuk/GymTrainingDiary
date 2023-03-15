using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymTrainingDiary.Utilities.Metadata
{
    public class ActionParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string Source { get; set; }

        public int Position { get; set; }
        public bool IsOptional { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public object DefaultValue { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("DefaultValue")]
        public object DefaultValueToSee => this.DefaultValue == default(object) ? this.DefaultValue : null;

    }
}
