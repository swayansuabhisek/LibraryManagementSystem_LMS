using FluentValidation;
using LMS.Shared.DTOs;

namespace LMS.Shared.DtoValidators;

public class CreateAuthorDtoValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(80);
        RuleFor(x => x.Description).Empty().MaximumLength(200);
    }
}