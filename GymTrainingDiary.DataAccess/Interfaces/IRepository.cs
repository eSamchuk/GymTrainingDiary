using GymTrainingDiary.Utilities.DataWrappers;

namespace GymTrainingDiary.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        bool IsItemsExistForCondition(Func<T, bool> condition);

        IEnumerable<T> GetAllItems();

        IEnumerable<T> GetItemsByCondtion(Func<T, bool> condition);

        PaginationInfo<T> GetPagedItems(int page, int perPage);

        T GetItemById(int id);

        T AddItem(T newItem);

        T UpdateItem(T itemToUpdate);

        bool DeleteItem(int itemId);
    }
}
