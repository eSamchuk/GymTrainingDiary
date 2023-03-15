using GymTrainingDiary.DataHandling.Converters;
using System.Text.Json.Serialization;

namespace GymTrainingDiary.DataAccess.DTO
{
    public class UserDTO : DiscoverableDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public bool HasTrainer { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime SignupDate { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastVisit { get; set; }

    }
}
