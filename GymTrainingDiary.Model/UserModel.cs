using System.ComponentModel.DataAnnotations;

namespace GymTrainingDiary.Model
{
    public class UserModel
    {
        public UserModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
