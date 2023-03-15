using GymTrainingDiary.Data;
using GymTrainingDiary.Data.Entities;
using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.Utilities.DataWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.DataAccess.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly GymTrainingDataContext dbContext;

        public ExerciseRepository(GymTrainingDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Exercise AddItem(Exercise newItem)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exercise> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Exercise GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exercise> GetItemsByCondtion(Func<Exercise, bool> condition)
        {
            return this.dbContext.Excercises.Where(condition).ToList();
        }

        public PaginationInfo<Exercise> GetPagedItems(int page, int perPage)
        {
            throw new NotImplementedException();
        }

        public bool IsItemsExistForCondition(Func<Exercise, bool> condition)
        {
            return this.dbContext.Excercises.Any(condition);
        }

        public Exercise UpdateItem(Exercise itemToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
