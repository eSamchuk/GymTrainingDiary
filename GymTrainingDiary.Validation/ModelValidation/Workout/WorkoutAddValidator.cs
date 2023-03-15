using FluentValidation;
using GymTrainingDiary.Model;

namespace GymTrainingDiary.Validation.ModelValidation.Workout
{
    public class WorkoutAddValidator : AbstractValidator<WorkoutModel>
    {
        public WorkoutAddValidator()
        {
            RuleFor(x => x.UserId).NotEqual(0);
            RuleFor(x => x.WorkoutStart).NotEmpty().LessThan(x => x.WorkoutEnd);
            RuleFor(x => x.WorkoutEnd).NotEmpty().GreaterThan(x => x.WorkoutStart);
        }
    }
}
