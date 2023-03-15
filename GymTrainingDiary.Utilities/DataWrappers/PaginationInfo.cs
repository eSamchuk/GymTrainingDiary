namespace GymTrainingDiary.Utilities.DataWrappers
{
    public class PaginationInfo<T> where T : class, new()
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public List<T>? Items { get; set; } = new List<T>();
    }
}
