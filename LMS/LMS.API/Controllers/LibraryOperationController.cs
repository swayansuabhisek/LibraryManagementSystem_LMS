using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;
using LMS.Shared.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[ApiController]
[Route("api/libraryops")]
public class LibraryOperationController : ControllerBase
{
    private readonly ILibraryOperationService _libraryOperationService;
    private readonly ILogger<LibraryOperationController> _logger;

    public LibraryOperationController(ILibraryOperationService libraryOperationService, ILogger<LibraryOperationController> logger)
    {
        _libraryOperationService = libraryOperationService;
        _logger = logger;
    }
    
    [HttpPost("borrow-book")]
    public async Task<IActionResult> BorrowBook(CreateBorrowBookDto borrowBookDto)
    {
        _logger.LogInformation("Inside BorrowBook method");
        await _libraryOperationService.BorrowBookAsync(borrowBookDto);
        return Ok(new APIResponse<object>
        {
            IsSuccess = true,
            Message = "Book borrow successful"
        });
    }

    [HttpPost("return-book")]
    public async Task<IActionResult> ReturnBook(ReturnBookDto returnBookDto)
    {
        _logger.LogInformation("Inside ReturnBook method");
        await _libraryOperationService.ReturnBookAsync(returnBookDto);
        return Ok(new APIResponse<object>
        {
            IsSuccess = true,
            Message = "Book return successful"
        });
    }

    [HttpGet("borrow-book-records")]
    public async Task<IActionResult> AllBorrowBookRecords()
    {
        _logger.LogInformation("Inside AllBorrowBookRecords method");
        var result = await _libraryOperationService.AllBorrowRecordsAsync();
        return Ok(new APIResponse<List<BorrowBookDto>>()
        {
            IsSuccess = true,
            Message = "All borrow book records retrived successful",
            Data = result
        });

    }
}