using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace GymTrainingDiary.Model
{
    public class WorkoutModel
    {
        public WorkoutModel(int id, int userId, DateTime workoutStart, DateTime workoutEnd)
        {
            Id = id;
            UserId = userId;
            WorkoutStart = workoutStart;
            WorkoutEnd = workoutEnd;
        }

        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime WorkoutStart { get; set; }

        [Required]
        public DateTime WorkoutEnd { get; set; }
    }
}
