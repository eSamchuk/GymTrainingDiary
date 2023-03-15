using GymTrainingDiary.Abstractions;
using GymTrainingDiary.Data;
using GymTrainingDiary.Data.Entities;
using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.Utilities.DataWrappers;
using Microsoft.EntityFrameworkCore;

namespace GymTrainingDiary.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GymTrainingDataContext dbContext;

        public UserRepository(GymTrainingDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User AddItem(User newItem)
        {
            var result = this.dbContext.Users.Add(newItem);
            this.dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteItem(int itemId)
        {
            var itemToDelete = this.dbContext.Users.Find(itemId);

            if (itemToDelete == null) return false;

            var res = this.dbContext.Users.Remove(itemToDelete);
            this.dbContext.SaveChanges();
            return res.State == EntityState.Deleted;
        }

        public IEnumerable<User> GetAllItems()
        {
            return this.dbContext.Users.ToList();
        }

        public User GetItemById(int id)
        {
            return this.dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetItemsByCondtion(Func<User, bool> condition)
        {
            return this.dbContext.Users.Where(condition).ToList();
        }

        public PaginationInfo<User> GetPagedItems(int page, int perPage)
        {
            var result = new PaginationInfo<User>();

            result.CalculateFields(this.dbContext.Users.Count(), page, perPage);
            result.Items = this.dbContext.Users
                            .Skip((page - 1) * perPage)
                            .Take(perPage)
                            .ToList();

            return result;
        }

        public bool IsItemsExistForCondition(Func<User, bool> condition)
        {
            return this.dbContext.Users.Any(condition);
        }

        public bool IsUserExist(string firstName, string lastName)
        {
            return this.dbContext.Users.Any(x => x.FirstName.ToUpper() == firstName.ToUpper() &&
                                                 x.LastName.ToUpper() == lastName.ToUpper());
        }

        public User UpdateItem(User itemToUpdate)
        {
            var existingItem = this.GetItemById(itemToUpdate.Id);

            if (existingItem != null) return null;

            existingItem.FirstName = itemToUpdate.FirstName;
            existingItem.LastName = itemToUpdate.LastName;

            this.dbContext.Users.Update(existingItem);
            this.dbContext.SaveChanges();

            return existingItem;
        }
    }
}
