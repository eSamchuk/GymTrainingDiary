using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Data
{
    public static partial class DataAccessMethods
    {
        private static void DoInTransaction(Action<GymTrainingDiaryContext> expression)
        {
            using (var db = new GymTrainingDiaryContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        expression(db);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static TResult RunQuery<TResult>(Func<GymTrainingDiaryContext, TResult> expression) where TResult : class
        {
            using (var db = new GymTrainingDiaryContext())
            {
                try
                {
                    var result = expression(db);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
