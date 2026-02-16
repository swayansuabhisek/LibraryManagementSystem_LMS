using FluentValidation;
using LMS.Shared.DTOs;

namespace LMS.API.DTOs;

public class UpdateBorrowerDtoValidator : AbstractValidator<UpdateBorrowerDto>
{
    public UpdateBorrowerDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty").MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty").EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty").MaximumLength(12);
    }
}