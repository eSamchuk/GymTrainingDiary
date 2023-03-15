using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GymTrainingDiary.Data.Entities
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? RequiredEquipmentId { get; set; }

        public Equipment? RequiredEquipment { get; set; }


        public List<WorkoutExercise>? WorkoutExercises { get; set; }
    }
}