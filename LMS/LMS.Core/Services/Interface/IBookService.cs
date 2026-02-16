using LMS.Shared.DTOs;
using LMS.Shared.Models;

namespace LMS.Core.Services.Interface;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByNumberAsync(int number);
    Task AddBookAsync(CreateBookDto book);
    Task UpdateBookAsync(UpdateBookDto book);
    Task DeleteBookAsync(int id);
}