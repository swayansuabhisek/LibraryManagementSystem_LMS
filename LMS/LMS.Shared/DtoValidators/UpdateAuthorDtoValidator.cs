using FluentValidation;
using LMS.Shared.DTOs;

namespace LMS.Shared.DtoValidators;

public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(80);
        RuleFor(x => x.Description).MaximumLength(200);
    }
}