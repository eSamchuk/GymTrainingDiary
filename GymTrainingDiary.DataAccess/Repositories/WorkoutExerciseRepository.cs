using GymTrainingDiary.Data;
using GymTrainingDiary.Data.Entities;
using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.Utilities.DataWrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.DataAccess.Repositories
{
    public class WorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        private readonly GymTrainingDataContext dbContext;

        public WorkoutExerciseRepository(GymTrainingDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public WorkoutExercise AddItem(WorkoutExercise newItem)
        {
            var result =  this.dbContext.WorkoutExercise.Add(newItem);
            this.dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteItem(int itemId)
        {
            var itemToDelete = this.dbContext.WorkoutExercise.Find(itemId);

            if (itemToDelete == null) return false;

            var result = this.dbContext.WorkoutExercise.Remove(itemToDelete);
             this.dbContext.SaveChanges();
            return result.State == EntityState.Deleted;
        }

        public IEnumerable<WorkoutExercise> GetAllItems()
        {
            return this.dbContext.WorkoutExercise.ToList();
        }

        public  WorkoutExercise GetItemById(int id)
        {
            return this.dbContext.WorkoutExercise.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<WorkoutExercise> GetItemsByCondtion(Func<WorkoutExercise, bool> condition)
        {
            return  this.dbContext.WorkoutExercise
            .Include(x => x.Excercise)
            .Where(condition)
            .ToList();
        }

        public PaginationInfo<WorkoutExercise> GetPagedItems(int page, int perPage)
        {
            return null;
        }

        public bool IsItemsExistForCondition(Func<WorkoutExercise, bool> condition)
        {
            return this.dbContext.WorkoutExercise.Any(condition);
        }

        public WorkoutExercise UpdateItem(WorkoutExercise itemToUpdate)
        {
            var existingItem =  this.dbContext.WorkoutExercise.Find(itemToUpdate.Id);

            if (existingItem == null) return null;

            existingItem.ExcerciseId = itemToUpdate.ExcerciseId;
            existingItem.WorkoutId = itemToUpdate.WorkoutId;

            this.dbContext.WorkoutExercise.Update(itemToUpdate);
            this.dbContext.SaveChanges();

            return existingItem;
        }
    }
}
