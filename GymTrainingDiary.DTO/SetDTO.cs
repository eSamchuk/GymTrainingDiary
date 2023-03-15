using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.DTO
{
    public class SetDTO
    {
        public int Id { get; set; }

        public int Weight { get; set; }

        public int Reps { get; set; }
    }
}
