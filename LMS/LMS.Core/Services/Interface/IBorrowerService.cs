using LMS.Shared.DTOs;
using LMS.Shared.Models;

namespace LMS.Core.Services.Interface;

public interface IBorrowerService
{
    Task<List<BorrowerDto>> GetAllBorrowersAsync();
    Task<BorrowerDto?> GetBorrowerByIdAsync(int id);
    Task AddBorrowerAsync(CreateBorrowerDto borrower);
    Task UpdateBorrowerAsync(UpdateBorrowerDto borrower);
    Task DeleteBorrowerAsync(int id);
}