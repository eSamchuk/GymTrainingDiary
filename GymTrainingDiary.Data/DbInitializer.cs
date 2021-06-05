using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<GymTrainingDiaryContext>
    {
        protected override void Seed(GymTrainingDiaryContext context)
        {
            context.CommonTypes.Add(
                new Model.CommonType
                {
                    Domain = "AccountType",
                    Name = "Trainer"
                });

            context.CommonTypes.Add(
               new Model.CommonType
               {
                   Domain = "AccountType",
                   Name = "User"
               });

            context.SaveChanges();

        }
    }
}
