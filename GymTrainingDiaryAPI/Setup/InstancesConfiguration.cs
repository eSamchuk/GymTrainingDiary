using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.DataAccess.Repositories;
using GymTrainingDiary.Utilities.ActionFifters;
using Serilog;

namespace GymTrainingDiaryAPI.Setup
{
    public static class InstancesConfiguration
    {
        public static void ConfigureInstances(this IServiceCollection services)
        {
            services.AddSingleton(new CatchTimedOutRequestFilter());
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IEquipmentRepository, EquipmentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWorkoutRepository, WorkoutsRepository>();
            services.AddTransient<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
            services.AddSingleton(Log.Logger);
        }
    }
}
