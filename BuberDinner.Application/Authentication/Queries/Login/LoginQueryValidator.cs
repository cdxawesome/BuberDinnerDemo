using FluentValidation;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("邮箱不能为空")
                .EmailAddress().WithMessage("邮箱格式不正确");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("密码不能为空");
        }
    }
}