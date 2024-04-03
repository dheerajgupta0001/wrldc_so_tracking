using FluentValidation;

namespace Application.Pams.Commands.CreatePam;

public class CreatePamCommandValidator : AbstractValidator<CreatePamCommand>
{
    public CreatePamCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
    }
}
