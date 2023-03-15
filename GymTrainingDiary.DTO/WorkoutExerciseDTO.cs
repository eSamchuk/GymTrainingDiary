using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.DTO
{
    public class WorkoutExerciseDTO
    {
        public int Id { get; set; }

        public string? Excercise { get; set; }

        public List<SetDTO>? Sets { get; set; }
    }
}
