using FluentValidation;
using LMS.Shared.DTOs;

namespace LMS.Shared.DtoValidators;


public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
{
    public UpdateBookDtoValidator()
    {
        RuleFor(x => x.Number).NotEmpty().WithMessage("Number cannot be empty");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty").MaximumLength(80);
        RuleFor(x=>x.AuthorId).NotEmpty().WithMessage("AuthorId cannot be empty").GreaterThan(0);
        RuleFor(x=>x.TotalCopies).NotEmpty().WithMessage("TotalCopies cannot be empty").GreaterThan(0);
    }
}