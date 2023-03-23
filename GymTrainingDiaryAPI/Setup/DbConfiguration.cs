using GymTrainingDiary.Data;
using Microsoft.EntityFrameworkCore;

namespace GymTrainingDiaryAPI.Setup
{
    public static class DbConfiguration
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GymTrainingDataContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("GymConnectionString"));
            }, ServiceLifetime.Scoped);
        }
    }
}
