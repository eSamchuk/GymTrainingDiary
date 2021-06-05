using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTrainingDiary.Data.Model
{
    public class Training
    {
        public decimal Id { get; set; }


        [Required]
        public DateTime TrainingStart { get; set; }
        [Required]
        public DateTime TrainingEnd { get; set; }

        [Required]
        public decimal UserId { get; set; }
        public virtual User User { get; set; }


        public virtual List<Excercise> TrainingExcercises { get; set; }
    }
}