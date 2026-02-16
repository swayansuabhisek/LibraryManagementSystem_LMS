using LMS.Infrastructure.Repositories.Interface;
using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;
using Microsoft.Extensions.Logging;

namespace LMS.Service.Services;

public class LibraryOperationService : ILibraryOperationService
{
    private readonly IBookRepository _bookRepository;
    private readonly IBorrowerRepository _borrowerRepository;
    private readonly IBorrowBookRepository  _borrowBookRepository;
    private readonly ILogger<LibraryOperationService> _logger;

    public LibraryOperationService(
        IBookRepository bookRepository,
        IBorrowerRepository borrowerRepository,
        IBorrowBookRepository borrowBookRepository,
        ILogger<LibraryOperationService> logger
        )
    {
        _bookRepository = bookRepository;
        _borrowerRepository = borrowerRepository;
        _borrowBookRepository = borrowBookRepository;
        _logger = logger;
    }
    public async Task BorrowBookAsync(CreateBorrowBookDto createBorrowBookDto)
    {
        var isAlreadyBorrowed =
            await _borrowBookRepository.CheckDuplicateBorrowBookData(createBorrowBookDto.BookId,
                createBorrowBookDto.BorrowerId);
        if (isAlreadyBorrowed)
        {
            _logger.LogInformation("User Already Borrowed the same book");
            throw new Exception("User Already Borrowed the same book");
        }
        var book = await _bookRepository.GetBookByNumber(createBorrowBookDto.BookId);
        if (book == null)
        {
            _logger.LogInformation("Book not found");
            throw new ArgumentException("Book not found");
        }

        var borrower = await _borrowerRepository.GetBorrowerById(createBorrowBookDto.BorrowerId);
        if (borrower == null)
        {
            _logger.LogInformation("Borrower not found");
            throw new ArgumentException("Borrower not found");
        }

        var borrowBookEntity = new BorrowBook()
        {
            BookId = createBorrowBookDto.BookId,
            BorrowerId = createBorrowBookDto.BorrowerId,
            BookIssueDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(30), //due date is 30 days after issuing the book
            IsReturned = false
        };
        book.AvailableCopies--;
        book.IsAvailable = book.AvailableCopies > 0 ? true : false;

        await _borrowBookRepository.AddBorrowBookData(borrowBookEntity);
        _logger.LogInformation($"BorrowBook Added BorrowBook: [BookId={borrowBookEntity.BookId}, BorrowerId={borrowBookEntity.BorrowerId}, BookIssueDate={borrowBookEntity.BookIssueDate}, DueDate={borrowBookEntity.DueDate}, IsReturned={borrowBookEntity.IsReturned}]");
        await _bookRepository.UpdateBook(book);
        _logger.LogInformation($"Book Updated BorrowBook: [BookId={borrowBookEntity.BookId}, BorrowerId={borrowBookEntity.BorrowerId}, BookIssueDate={borrowBookEntity.BookIssueDate}, DueDate={borrowBookEntity.DueDate}, IsReturned={borrowBookEntity.IsReturned}]");

    }

    public async Task ReturnBookAsync(ReturnBookDto returnBookDto)
    {
        var borrowBookData = await _borrowBookRepository.GetBorrowBookDataByBookAndBorrowerId(returnBookDto.BookId, returnBookDto.BorrowerId);
        if (borrowBookData == null)
        {
            _logger.LogInformation("Borrow Book Data not found");
            throw new ArgumentException("Borrow Book Data not found");
        }
        
        var book = await _bookRepository.GetBookByNumber(borrowBookData.BookId);
        if (book == null)
        {
            _logger.LogInformation("Book not found");
            throw new ArgumentException("Book not found");
        }
        
        book.AvailableCopies++;
        book.IsAvailable = book.AvailableCopies > 0 ? true : false;
        borrowBookData.IsReturned = true;
        borrowBookData.ReturnDate = DateTime.Now;
        
        await _borrowBookRepository.UpdateBorrowBookData(borrowBookData);
        await _bookRepository.UpdateBook(book);
    }

    public async Task<List<BorrowBookDto>> AllBorrowRecordsAsync()
    {
        var borrowBookData = await _borrowBookRepository.GetBorrowBooksAsync();
        var borrowBookDtos = borrowBookData.Select(bbd => new BorrowBookDto()
        {
            Id = bbd.BookId,
            BookId = bbd.BookId,
            BookTitle = bbd.Book.Title,
            BorrowerId = bbd.BorrowerId,
            BorrowerName = bbd.Borrower.Name,
            IssuedDate = bbd.BookIssueDate,
            ReturnDate = bbd.ReturnDate,
            IsReturned = bbd.IsReturned
        }).ToList();
        return borrowBookDtos;
    }
}