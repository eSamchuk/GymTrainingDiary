using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTrainingDiary.Data.Entities
{
    public class User
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? TrainerId { get; set; }

        public Trainer? Trainer { get; set; }

        [Required]
        public DateTime SignupDate { get; set; }

        public DateTime LastVisit { get; set; }

        public List<Workout>? Workouts { get; set; }
    }
}
