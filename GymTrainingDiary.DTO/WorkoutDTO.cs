using GymTrainingDiary.DataHandling.Converters;
using System.Text.Json.Serialization;

namespace GymTrainingDiary.DTO
{
    public class WorkoutDTO : DiscoverableDTO
    {
        public int Id { get; set; }
        public string User { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime WorkoutStart { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime WorkoutEnd { get; set; }

        public int Duration { get; set; }

        public int TotalExercises { get; set; }

        public List<WorkoutExerciseDTO>? Exercises { get; set; }
    }
}