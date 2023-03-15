using GymTrainingDiary.Data.Entities;
using GymTrainingDiary.DTO;

namespace GymTrainingDiary.Mapping.EntityToDto
{
    public static class EntitiesToDtoMapper
    {
        public static WorkoutDTO MapWorkoutToDto(this Workout original) => new WorkoutDTO
        {
            Id = original.Id,
            Duration = original.Duration,
            User = original.User != null ? $"{original.User?.FirstName} {original.User?.LastName}" : null,
            WorkoutStart = original.WorkoutStart,
            WorkoutEnd = original.WorkoutEnd
        };

        public static UserDTO MapUserToDto(this User original) => new UserDTO
        {
            Id = original.Id,
            FullName = $"{original.FirstName} {original.LastName}",
            HasTrainer = original.TrainerId != null,
            SignupDate = original.SignupDate,
            LastVisit = original.LastVisit
        };

        public static WorkoutExerciseDTO MapWorkoutExerciseToDto(this WorkoutExercise source) => new WorkoutExerciseDTO
        {
            Id = source.Id,
            Excercise = source.Excercise?.Name
        };

        public static EquipmentDTO MapEquipmentToDTO(this Equipment source) => new EquipmentDTO
        {
            Id = source.Id,
            Name = source.Name
        };
    }
}
