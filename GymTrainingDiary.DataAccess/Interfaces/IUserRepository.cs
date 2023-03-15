using GymTrainingDiary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExist(string firstName, string lastName);
    }
}
