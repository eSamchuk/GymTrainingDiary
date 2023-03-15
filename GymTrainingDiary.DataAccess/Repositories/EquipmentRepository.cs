using GymTrainingDiary.Abstractions;
using GymTrainingDiary.Data;
using GymTrainingDiary.Data.Entities;
using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.Utilities.DataWrappers;

namespace GymTrainingDiary.DataAccess.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly GymTrainingDataContext dbContext;

        public EquipmentRepository(GymTrainingDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Equipment AddItem(Equipment newItem)
        {
            var result = dbContext.Equipment.Add(newItem);
            dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteItem(int itemId)
        {
            var itemToDelete = dbContext.Equipment.Find(itemId);
            dbContext.Equipment.Remove(itemToDelete);
            dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<Equipment> GetAllItems()
        {
            return dbContext.Equipment.ToList();
        }

        public Equipment GetItemById(int id)
        {
            return dbContext.Equipment.Find(id);
        }

        public IEnumerable<Equipment> GetItemsByCondtion(Func<Equipment, bool> condition)
        {
            return dbContext.Equipment.Where(condition);
        }

        public PaginationInfo<Equipment> GetPagedItems(int page, int perPage)
        {
            PaginationInfo<Equipment> result = new PaginationInfo<Equipment>();

            result.Items = dbContext.Equipment.Skip((page - 1) * perPage).Take(perPage).ToList();
            result.CalculateFields(result.Items.Count, page, perPage);

            return result;
        }

        public bool IsEqipmentExist(string name)
        {
            return this.dbContext.Equipment.Any(x => x.Name == name);
        }

        public bool IsItemsExistForCondition(Func<Equipment, bool> condition)
        {
            return this.dbContext.Equipment.Any(condition);
        }

        public Equipment UpdateItem(Equipment itemToUpdate)
        {
            ////TODO переписати поля

            dbContext.Equipment.Update(itemToUpdate);
            dbContext.SaveChanges();
            return itemToUpdate;
        }
    }
}
