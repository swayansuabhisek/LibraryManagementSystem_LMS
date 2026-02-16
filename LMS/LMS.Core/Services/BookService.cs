using LMS.Infrastructure.Repositories.Interface;
using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;

namespace LMS.Service.Services;

public class BookService: IBookService
{
    private readonly IBookRepository _bookRepository;
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllBooks();
        var bookDtos = books.Select(book => new BookDto()
        {
            BookNumber =  book.BookNumber,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = book.Author.Name,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies,
            IsAvailable = book.IsAvailable
        }).ToList();
        return bookDtos;
    }

    public async Task<BookDto?> GetBookByNumberAsync(int number)
    {
        var book = await _bookRepository.GetBookByNumber(number);
        var bookDto = book == null ? null : new BookDto()
        {
            BookNumber = book.BookNumber,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = book.Author.Name,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies,
            IsAvailable = book.IsAvailable
        };
        return bookDto;
    }

    public async Task AddBookAsync(CreateBookDto book)
    {
        var bookEntity = new Book()
        {
            Title = book.Title,
            AuthorId = book.AuthorId,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.TotalCopies
        };
        await _bookRepository.AddBook(bookEntity);
    }

    public async Task UpdateBookAsync(UpdateBookDto book)
    {
        var bookEntity = new Book()
        {
            BookNumber = book.Number,
            Title = book.Title,
            AuthorId = book.AuthorId,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.TotalCopies
        };
        await _bookRepository.UpdateBook(bookEntity);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteBook(id);
    }
}