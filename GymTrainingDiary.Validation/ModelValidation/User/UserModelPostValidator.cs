using FluentValidation;
using GymTrainingDiary.Model;

namespace GymTrainingDiary.Validation.ModelValidation
{
    public class UserModelPostValidator : AbstractValidator<UserModel2>
    {
        public UserModelPostValidator()
        {
            this.RuleFor(x => x.Id).Equal(0).WithMessage("New user should not have Id value provided"); ;
            this.RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            this.RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        }
    }
}
