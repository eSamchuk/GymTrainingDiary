using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTrainingDiary.Data.Entities
{
    public class WorkoutExercise
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ExcerciseId { get; set; }
        public Exercise? Excercise { get; set; }
     
        [Required]
        public int WorkoutId { get; set; }

        public Workout? Workout { get; set; }

        public List<Set>? Sets { get; set; }
    }
}
