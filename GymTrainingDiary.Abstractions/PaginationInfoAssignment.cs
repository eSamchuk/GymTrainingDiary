using GymTrainingDiary.Utilities.DataWrappers;

namespace GymTrainingDiary.Abstractions
{
    public static class PaginationInfoAssignment
    {
        public static PaginationInfo<T> CalculateFields<T>(this PaginationInfo<T> result, int totalItems, int page, int perPage) where T : class, new()
        {
            result.TotalCount = totalItems;
            result.TotalPages = (result.TotalCount / (double)perPage) % 1 != 0 ? (result.TotalCount / perPage) + 1 : (result.TotalCount / perPage);
            result.CurrentPage = page;
            result.PageSize = perPage;
            result.HasNext = page < result.TotalPages;
            result.HasPrevious = page > 1;

            return result;
        }
    }
}
