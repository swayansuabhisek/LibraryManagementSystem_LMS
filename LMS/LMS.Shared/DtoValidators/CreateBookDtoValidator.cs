using FluentValidation;
using LMS.Shared.DTOs;

namespace LMS.Shared.DtoValidators;


public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty").MaximumLength(80);
        RuleFor(x=>x.AuthorId).NotEmpty().WithMessage("AuthorId cannot be empty").GreaterThan(0);
        RuleFor(x=>x.TotalCopies).NotEmpty().WithMessage("TotalCopies cannot be empty").GreaterThan(0);
    }
}
