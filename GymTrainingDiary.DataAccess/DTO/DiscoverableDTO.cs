using GymTrainingDiary.Utilities.HATEOAS;
using System.Text.Json.Serialization;

namespace GymTrainingDiary.DataAccess.DTO
{
    public class DiscoverableDTO
    {
        [JsonPropertyOrder(99)]
        public List<Link> Links { get; set; }
    }
}
