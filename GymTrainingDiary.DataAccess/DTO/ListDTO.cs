using System.Text.Json.Serialization;

namespace GymTrainingDiary.DataAccess.DTO
{
    public class ListDTO<T> where T : class, new()
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}
