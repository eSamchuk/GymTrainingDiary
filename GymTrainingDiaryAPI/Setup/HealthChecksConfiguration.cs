namespace GymTrainingDiaryAPI.Setup
{
    public static class HealthChecksConfiguration
    {
        public static void ConfigureHealthChecks(this WebApplicationBuilder builder)
        {
            builder.Services
             .AddHealthChecks()
             .AddSqlServer(builder.Configuration.GetConnectionString("GymConnectionString"), name: "Gym")
             .AddSqlServer(builder.Configuration.GetConnectionString("RecipesDb"), name: "Recipes");
        }
    }
}
