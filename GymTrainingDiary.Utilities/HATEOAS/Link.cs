namespace GymTrainingDiary.Utilities.HATEOAS
{
    public class Link
    {
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string? HttpMethod { get; set; }

        public Link()
        {
        }

        public Link(string url, string description, string httpMethod)
        {
            this.Url = url;
            this.Description = description;
            this.HttpMethod = httpMethod;
        }
    }
}
