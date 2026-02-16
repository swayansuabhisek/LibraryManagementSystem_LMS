using FluentValidation;
using LMS.Shared.DTOs;

namespace LMS.API.DTOs;

public class ReturnBookDtoValidator : AbstractValidator<ReturnBookDto>
{
    public ReturnBookDtoValidator()
    {
        RuleFor(x => x.ReturnDate).NotEmpty().LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Return date cannot be the future date.");
    }
}