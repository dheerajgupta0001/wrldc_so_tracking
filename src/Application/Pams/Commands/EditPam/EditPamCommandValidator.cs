using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.Pams.Commands.EditPam
{ 
    public class EditPamCommandValidator : AbstractValidator<EditPamCommand>
        {
            public EditPamCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }
}