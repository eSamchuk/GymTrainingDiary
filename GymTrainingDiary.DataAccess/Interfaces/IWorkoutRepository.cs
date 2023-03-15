using GymTrainingDiary.Data.Entities;

namespace GymTrainingDiary.DataAccess.Interfaces
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        IEnumerable<Workout> GetWorkoutsForUserById(int id);
    }
}
