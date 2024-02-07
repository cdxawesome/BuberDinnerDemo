using FluentValidation;

namespace BuberDinner.Application.Authentication.Command.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("邮箱是必须的")
                .EmailAddress().WithMessage("邮箱格式不正确");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("密码是必须的")
                .MinimumLength(6).WithMessage("密码必须至少6位");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
        }
    }
}