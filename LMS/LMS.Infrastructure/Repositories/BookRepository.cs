using LMS.Infrastructure.Repositories.Interface;
using LMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class BookRepository: IBookRepository
{
    private readonly LibraryDbContext _dbContext;
    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Book>> GetAllBooks()
    {
        return await _dbContext.Books
            .Include(b => b.Author)
            .ToListAsync();
    }

    public async Task<Book?> GetBookByNumber(int number)
    {
        return await _dbContext.Books.Where(a => a.BookNumber == number)
            .Include(b => b.Author)
            .FirstOrDefaultAsync();
    }

    public async Task AddBook(Book book)
    {
        var author = await _dbContext.Authors.FindAsync(book.AuthorId);
        if (author == null)
        {
            throw new Exception("Invalid Author");
        }
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBook(Book book)
    {
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteBook(int id)
    {
        var existingBook = await _dbContext.Books.FindAsync(id);
        if (existingBook == null)
        {
            throw new Exception("Book not found");
        }
        _dbContext.Books.Remove(existingBook);
        await _dbContext.SaveChangesAsync();
    }
}