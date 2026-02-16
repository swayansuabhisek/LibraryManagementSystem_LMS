using Microsoft.EntityFrameworkCore;
using LMS.Infrastructure;
using LMS.Shared.Models;

namespace LMS.UnitTest;

public class BookTests
{
    private LibraryDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new LibraryDbContext(options);
    }
    [Fact]
    public async Task AddBook_Should_Increase_BookCount()
    {
        var dbContext = GetDbContext();

        var book = new Book
        {
            Title = "Test_Book",
            AuthorId = 1,
            TotalCopies = 2,
            AvailableCopies = 2
        };
        await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();
        var bookCount = await dbContext.Books.CountAsync();
        Assert.Equal(1, bookCount);
    }
}