using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTrainingDiary.Data.Model
{
    public class ExcerciseType
    {
        public decimal Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual List<Excercise> Excercises { get; set; }




    }
}