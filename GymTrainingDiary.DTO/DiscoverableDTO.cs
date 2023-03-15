using GymTrainingDiary.Utilities.HATEOAS;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace GymTrainingDiary.DTO
{
    public class DiscoverableDTO
    {
        //[JsonPropertyOrder(99)]
        //[XmlElement(Order = 99)]
        public List<Link> Links { get; set; }
    }
}
