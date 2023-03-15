using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymTrainingDiary.DataHandling
{
    public static class ModelStateErrorsHandler
    {
        public static IEnumerable<string> FormatErrors(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(y => y.Value.Errors.Select(z => z.ErrorMessage)).ToList();
        }
    }
}