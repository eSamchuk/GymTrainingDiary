using GymTrainingDiary.Data.Entities;
using GymTrainingDiary.Model;

namespace GymTrainingDiary.Mapping.ModelToEntity
{
    public static class ModelToEntityMapper
    {
        public static Workout MapWorkoutModelToEntity(this WorkoutModel model)
        {
            return new Workout() { 
                Id = model.Id, 
                UserId = model.UserId, 
                WorkoutStart = model.WorkoutStart, 
                WorkoutEnd = model.WorkoutEnd
            };
        }

        public static User MapUserModelToEntity(this UserModel model)
        {
            return new User()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public static Equipment MapEquipmentModelToEntity(this EquipmentModel model)
        {
            return new Equipment()
            {
                Name = model.Name
            };
        }
    }
}
