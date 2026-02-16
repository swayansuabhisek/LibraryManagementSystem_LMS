using LMS.Shared.Models;

namespace LMS.Infrastructure.Repositories.Interface;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooks();
    Task<Book?> GetBookByNumber(int number);
    Task AddBook(Book book);
    Task UpdateBook(Book book);
    Task DeleteBook(int id);
}