using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Data.Model
{
    public class PhotoContainer 
    {
        public decimal Id { get; set; }

        public byte[] BlobData { get; set; }

        [Required]
        public decimal UserId { get; set; }

        public virtual User User { get; set; }
    }
}
