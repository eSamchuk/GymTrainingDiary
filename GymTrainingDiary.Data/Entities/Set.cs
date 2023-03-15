using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTrainingDiary.Data.Entities
{
    public class Set
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public int Reps { get; set; }

        public int ExcerciseId { get; set; }
        public WorkoutExercise? WorkoutExercise { get; set; }
    }
}
