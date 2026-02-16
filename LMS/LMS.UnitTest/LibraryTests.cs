using LMS.Infrastructure;
using LMS.Infrastructure.Repositories;
using LMS.Service.Services;
using LMS.Shared.DTOs;
using LMS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace LMS.UnitTest;

public class LibraryTests
{
    private LibraryDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new LibraryDbContext(options);
    }

    [Fact]
    public async Task BorrowBook_Should_Decrease_BookCopiesCount_And_Create_BorrwBookData()
    {
        var dbContext = GetDbContext();
        
        var book = new Book
        {
            BookNumber = 1,
            Title = "Interview Ready",
            TotalCopies = 2,
            AvailableCopies = 2
        };

        var borrower = new Borrower
        {
            Id = 1,
            Name = "Test Borrower",
            Email = "test@test.com",
            PhoneNumber = "088888888"
        };

        dbContext.Books.AddAsync(book);
        dbContext.Borrowers.AddAsync(borrower);
        await dbContext.SaveChangesAsync();
        
        var bookRepository = new BookRepository(dbContext);
        var borrowerRepository = new BorrowerRepository(dbContext);
        var borrowRecordRepository = new BorrowBookRepository(dbContext);
        var logger = NullLogger<LibraryOperationService>.Instance;
        
        var _service = new LibraryOperationService(bookRepository, borrowerRepository, borrowRecordRepository, logger);

        var borrowBook = new CreateBorrowBookDto
        {
            BookId = 1,
            BorrowerId = 1
        };
        await _service.BorrowBookAsync(borrowBook);

        var updatedBook = await dbContext.Books.FindAsync(1);
        Assert.Equal(1, updatedBook.AvailableCopies);
    }

    [Fact]
    public async Task ReturnBook_Should_Increase_BookCopiesCount_And_Update_BorrowBookReturnData()
    {
        var dbContext = GetDbContext();
        var book = new Book
        {
            BookNumber = 1,
            Title = "Interview Ready",
            TotalCopies = 2,
            AvailableCopies = 2
        };

        var borrower = new Borrower
        {
            Id = 1,
            Name = "Test Borrower",
            Email = "test@test.com",
            PhoneNumber = "088888888"
        };

        dbContext.Books.AddAsync(book);
        dbContext.Borrowers.AddAsync(borrower);
        
        await dbContext.SaveChangesAsync();
        
        var bookRepository = new BookRepository(dbContext);
        var borrowerRepository = new BorrowerRepository(dbContext);
        var borrowRecordRepository = new BorrowBookRepository(dbContext);
        var logger = NullLogger<LibraryOperationService>.Instance;
        
        var _service = new LibraryOperationService(bookRepository, borrowerRepository, borrowRecordRepository, logger);
        var borrowBook = new CreateBorrowBookDto()
        {
            BookId = 1,
            BorrowerId = 1,
        };
        await _service.BorrowBookAsync(borrowBook);
        var updatedBookAfterBorrow = await dbContext.Books.FindAsync(1);
        Assert.Equal(1, updatedBookAfterBorrow.AvailableCopies);
        Assert.Equal(true, updatedBookAfterBorrow.IsAvailable);
        
        var returnBook = new ReturnBookDto
        {
            BookId = 1,
            BorrowerId = 1
        };
        await _service.ReturnBookAsync(returnBook);
        
        var updatedBookAfterReturn = await dbContext.Books.FindAsync(1);
        Assert.Equal(2, updatedBookAfterReturn.AvailableCopies);
        Assert.Equal(true, updatedBookAfterReturn.IsAvailable);
        
        var updatedBorrowBook = await dbContext.BorrowBooks.FirstOrDefaultAsync(id => id.BookId == returnBook.BookId && id.BorrowerId == borrower.Id);
        Assert.NotNull(updatedBorrowBook);
        Assert.Equal(true, updatedBorrowBook.IsReturned);
    }
}