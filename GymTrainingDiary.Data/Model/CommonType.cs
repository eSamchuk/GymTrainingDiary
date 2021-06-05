using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTrainingDiary.Data.Model
{
    public class CommonType
    {
        public decimal Id { get; set; }

        [Required]
        public string Domain { get; set; }

        [Required]
        public string Name { get; set; }


    }
}