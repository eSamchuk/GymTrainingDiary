using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTrainingDiary.Data.Model
{
    public class Excercise
    {
        public decimal Id { get; set; }

        [Required]
        public decimal TrainingId { get; set; }

        [Required]
        public decimal ExcerciseTypeId { get; set; }

        public ExcerciseType ExcerciseType { get; set; }

        public virtual Training Training { get; set; }

    }
}