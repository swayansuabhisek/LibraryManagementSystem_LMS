using FluentValidation;
using LMS.Shared.DTOs;

namespace LMS.API.DTOs;

public class CreateBorrowerDtoValidator : AbstractValidator<CreateBorrowerDto>
{
    public CreateBorrowerDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty").MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty").MaximumLength(12);
    }
}