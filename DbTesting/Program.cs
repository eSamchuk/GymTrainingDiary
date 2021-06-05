using GymTrainingDiary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new GymTrainingDiaryContext();

            var s = t.Users.ToList();

        }
    }
}
