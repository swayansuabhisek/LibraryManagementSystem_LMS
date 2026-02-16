using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;
using LMS.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers;

[ApiController]
[Route("api/borrower")]
public class BorrowerController : ControllerBase
{
    private readonly ILogger<BorrowerController> _logger;
    private readonly IBorrowerService _borrowerService;

    public BorrowerController(ILogger<BorrowerController> logger, IBorrowerService borrowerService)
    {
        _logger = logger;
        _borrowerService = borrowerService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllBorrowers()
    {
        _logger.LogInformation("Inside GetAllBorrowers");
        var result = await _borrowerService.GetAllBorrowersAsync();
        return Ok(new APIResponse<List<BorrowerDto>>()
        {
            IsSuccess = true,
            Message = "All Borrower Retrived successful",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBorrowerById(int id)
    {
        _logger.LogInformation("Inside GetBorrowerById");
        var result = await _borrowerService.GetBorrowerByIdAsync(id);
        return Ok(new APIResponse<BorrowerDto>()
        {
            IsSuccess = true,
            Message = "Borrower found successfully",
            Data = result
        });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBorrower(UpdateBorrowerDto borrower)
    {
        _logger.LogInformation("Inside UpdateBorrower");
        await _borrowerService.UpdateBorrowerAsync(borrower);
        return Ok(new APIResponse<Borrower>()
        {
            IsSuccess = true,
            Message = "Borrower updated successfully"
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddBorrower(CreateBorrowerDto borrower)
    {
        _logger.LogInformation("Inside AddBorrower");
        await _borrowerService.AddBorrowerAsync(borrower);
        return Ok(new APIResponse<Borrower>()
        {
            IsSuccess = true,
            Message = "Borrower added successfully"
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrower(int id)
    {
        _logger.LogInformation("Inside DeleteBorrower");
        await _borrowerService.DeleteBorrowerAsync(id);
        return Ok(new APIResponse<Borrower>()
        {
            IsSuccess = true,
            Message = "Borrower deleted successfully"
        });
    }
}