using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;
using LMS.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[ApiController]
[Route("api/author")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly ILogger<AuthorController> _logger;
    public AuthorController(ILogger<AuthorController> logger,  IAuthorService authorService)
    {
        _logger = logger;
        _authorService = authorService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        _logger.LogInformation("Inside Getting all authors");
        var result = await _authorService.GetAllAuthorsAsync();
        return Ok(new APIResponse<List<AuthorDto>>()
        {
            IsSuccess = true,
            Message = "All Author Retrived successful",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        _logger.LogInformation("Inside Getting author by id");
        var result = await _authorService.GetAuthorByIdAsync(id);
        return Ok(new APIResponse<AuthorDto>()
        {
            IsSuccess = true,
            Message = "Author Retrived successful",
            Data = result
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorDto author)
    {
        _logger.LogInformation("Inside Updating author");
        await _authorService.UpdateAuthorAsync(author);
        return Ok(new APIResponse<Author>()
        {
            IsSuccess = true,
            Message = "Author Updated successful",
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorDto author)
    {
        _logger.LogInformation("Inside Adding author");
        await _authorService.AddAuthorAsync(author);
        
        return Ok(new APIResponse<Author>()
        {
            IsSuccess = true,
            Message = "Author Added successful",
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        _logger.LogInformation("Inside Deleting author by id");
        await _authorService.DeleteAuthorAsync(id);
        return Ok(new APIResponse<Author>()
        {
            IsSuccess = true,
            Message = "Author Deleted successful",
        });
    }
}