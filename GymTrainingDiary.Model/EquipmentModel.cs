using System.ComponentModel.DataAnnotations;

namespace GymTrainingDiary.Model
{
    public class EquipmentModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
