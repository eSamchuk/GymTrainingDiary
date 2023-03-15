using GymTrainingDiary.Data.Entities;

namespace GymTrainingDiary.DataAccess.Interfaces
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        bool IsEqipmentExist(string name);
    }
}
