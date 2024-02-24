using FluentValidation;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
    public CreateMenuCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("名称是必须的。");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("描述是必须的。");
        RuleFor(x => x.Sections)
            .NotEmpty().WithMessage("Sections is required.");
    }
}