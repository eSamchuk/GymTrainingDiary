using GymTrainingDiary.Abstractions;
using GymTrainingDiary.Data;
using GymTrainingDiary.Data.Entities;
using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.Utilities.DataWrappers;
using Microsoft.EntityFrameworkCore;

namespace GymTrainingDiary.DataAccess.Repositories
{
    public class WorkoutsRepository : IWorkoutRepository
    {
        private readonly GymTrainingDataContext dbContext;

        public WorkoutsRepository(GymTrainingDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Workout AddItem(Workout newItem)
        {
            var result = this.dbContext.Workouts.Add(newItem);
            this.dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteItem(int itemId)
        {
            var itemToDelete = this.dbContext.Workouts.Find(itemId);
            if (itemToDelete != null)
            {
                var res = this.dbContext.Workouts.Remove(itemToDelete);
                this.dbContext.SaveChanges();
                return res.State == EntityState.Deleted;
            }

            return false;
        }

        public IEnumerable<Workout> GetAllItems()
        {
            return this.dbContext.Workouts.Include(x => x.User).ToList();
        }

        public Workout GetItemById(int id)
        {
            return this.dbContext.Workouts.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Workout> GetItemsByCondtion(Func<Workout, bool> predicate)
        {
            return this.dbContext.Workouts.Where(predicate).ToList();
        }

        public IEnumerable<Workout> GetWorkoutsForUserById(int userId)
        {
            return this.dbContext.Workouts.Where(x => x.UserId == userId).ToList();
        }

        public Workout UpdateItem(Workout itemToUpdate)
        {
            var existingItem = this.dbContext.Workouts.Find(itemToUpdate.Id);

            if (existingItem == null) return null;

            existingItem.UserId = itemToUpdate.UserId;
            existingItem.WorkoutStart = itemToUpdate.WorkoutStart;
            existingItem.WorkoutEnd = itemToUpdate.WorkoutEnd;

            this.dbContext.Workouts.Update(existingItem);
            this.dbContext.SaveChanges();
            return existingItem;
        }

        public PaginationInfo<Workout> GetPagedItems(int page, int perPage)
        {
            var result = new PaginationInfo<Workout>();

            result.Items = this.dbContext.Workouts
                            .Skip((page - 1) * perPage)
                            .Take(perPage)
                            .Include(x => x.User)
                            .ToList();
            result.CalculateFields(result.Items.Count(), page, perPage);

            return result;
        }

        public bool IsItemsExistForCondition(Func<Workout, bool> condition)
        {
            return this.dbContext.Workouts.Any(condition);
        }
    }
}