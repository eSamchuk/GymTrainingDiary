using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Data.Model
{
    public class User
    {
        public decimal Id { get; set; }

        [Required]
        public decimal AccountTypeId { get; set; }

        public decimal? PhotoContainerId { get; set; }

        public virtual PhotoContainer PhotoContainer { get; set; }

        public CommonType AccountType { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public DateTime? LastLoginDate { get; set; }


        public virtual List<Training> UserTrainings { get; set; }

    }
}
