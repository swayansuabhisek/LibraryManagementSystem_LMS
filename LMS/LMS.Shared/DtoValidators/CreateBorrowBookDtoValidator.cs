using FluentValidation;
using LMS.Shared.DTOs;

public class CreateBorrowBookDtoValidator : AbstractValidator<CreateBorrowBookDto>
{
    public CreateBorrowBookDtoValidator()
    {
        RuleFor(x => x.BookId).GreaterThan(0);
        RuleFor(x => x.BorrowerId).GreaterThan(0);
    }
}