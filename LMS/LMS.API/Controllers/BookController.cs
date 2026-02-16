using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;
using LMS.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<AuthorController> _logger;
    public BookController(ILogger<AuthorController> logger,  IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        _logger.LogInformation("Inside Get all books");
        var result = await _bookService.GetAllBooksAsync();
        return Ok(new APIResponse<List<BookDto>>()
        {
            IsSuccess = true,
            Message = "All Book Retrived successful",
            Data = result
        });
    }

    [HttpGet("{number}")]
    public async Task<IActionResult> GetBookByNumber(int number)
    {
        _logger.LogInformation("Inside Get book by number");
        var result = await _bookService.GetBookByNumberAsync(number);
        return Ok(new APIResponse<BookDto>()
        {
            IsSuccess = true,
            Message = "Book found successfully",
            Data = result
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBook([FromBody]UpdateBookDto book)
    {
        await _bookService.UpdateBookAsync(book);
        return Ok(new APIResponse<Book>()
        {
            IsSuccess = true,
            Message = "Book updateed successfully"
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody]CreateBookDto book)
    {
        await _bookService.AddBookAsync(book);
        return Ok(new APIResponse<Book>()
        {
            IsSuccess = true,
            Message = "Book added successfully"
        });
    }
    [HttpDelete("{number}")]
    public async Task<IActionResult> DeleteBook(int number)
    {
        await _bookService.DeleteBookAsync(number);
        return Ok(new APIResponse<Book>()
        {
            IsSuccess = true,
            Message = "Book deleted successfully"
        });
    }
}