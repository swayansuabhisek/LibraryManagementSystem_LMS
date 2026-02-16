using LMS.Shared.DTOs;

namespace LMS.Core.Services.Interface;

public interface ILibraryOperationService
{
    Task BorrowBookAsync(CreateBorrowBookDto createBorrowBookDto);
    Task ReturnBookAsync(ReturnBookDto returnBookDto);
    Task<List<BorrowBookDto>> AllBorrowRecordsAsync();
}