using GymTrainingDiary.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Runtime.CompilerServices;
using GymTrainigDiary.StaticData;

namespace GymTrainingDiary.Data
{
    public static partial class DataAccessMethods
    {
        public static IEnumerable<User> GetUsers()
        {
            return RunQuery(db =>
            {
                return db.Users.Include(x => x.AccountType).ToList();
            });
        }

        public static User GetUserById(decimal Id)
        {
            return RunQuery(db =>
            {
                return db.Users.Include(x => x.AccountType).FirstOrDefault(x => x.Id == Id);
            });
        }

        public static IEnumerable<CommonType> GetAccountTypes()
        {
            return RunQuery(db =>
            {
                return db.CommonTypes.Where(x => x.Domain == AccountTypes.Domain).ToList();
            });
        }

        public static decimal AddOrUpdateUser(User user)
        {
            decimal res = 0;
            DoInTransaction(db =>
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
                res = user.Id;
            });

            return res;
        }

        public static void DeleteUser(decimal id)
        {
            DoInTransaction(db =>
            {
                db.Users.Remove(GetUserById(id));
                db.SaveChanges();
            });
        }

    }
}
