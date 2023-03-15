using FluentValidation;
using GymTrainingDiary.Model;

namespace GymTrainingDiary.Validation.ModelValidation.User
{
    public class UserModelPutValidator : AbstractValidator<UserModel2>
    {
        public UserModelPutValidator()
        {
            this.RuleFor(x => x.Id).NotEqual(0).WithMessage("Id is required to update an existing User");
            this.RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            this.RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
        }
    }
}
