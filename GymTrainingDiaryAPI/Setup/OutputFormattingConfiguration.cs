using System.Text.Json;
using System.Text.Json.Serialization;

namespace GymTrainingDiaryAPI.Setup
{
    public static class OutputFormattingConfiguration
    {
        public static void ConfigureOutputFormatting(this IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                opt.RespectBrowserAcceptHeader = true;
            })
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.WriteIndented = false;
                opt.JsonSerializerOptions.AllowTrailingCommas = false;
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                opt.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                opt.JsonSerializerOptions.UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement;
                opt.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
            })
            .AddXmlSerializerFormatters();
        }
    }
}
