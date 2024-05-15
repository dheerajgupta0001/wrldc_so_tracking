using Application.ReferenceNumbers.Commands.CreateReferenceNumber;
using FluentValidation;

public class CreateReferenceNumberCommandValidator : AbstractValidator<CreateReferenceNumberCommand>
{
    public CreateReferenceNumberCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
