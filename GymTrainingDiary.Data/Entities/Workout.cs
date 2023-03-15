using GymTrainingDiary.DataHandling.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymTrainingDiary.Data.Entities
{
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Required]
        public DateTime WorkoutStart { get; set; }

        [Required]
        public DateTime WorkoutEnd { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Duration { get; set; }
        
        public List<WorkoutExercise>? Excercises { get; set; }
    }
}
