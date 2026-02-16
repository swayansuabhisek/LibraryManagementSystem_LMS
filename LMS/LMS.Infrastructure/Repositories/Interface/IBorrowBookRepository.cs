using LMS.Shared.Models;

namespace LMS.Infrastructure.Repositories.Interface;

public interface IBorrowBookRepository
{
    Task<List<BorrowBook>> GetBorrowBooksAsync();
    Task AddBorrowBookData(BorrowBook borrowBook);
    Task<BorrowBook?> GetBorrowBookDataById(int id);
    Task<BorrowBook?> GetBorrowBookDataByBookAndBorrowerId(int bookId, int borrowerId);
    Task UpdateBorrowBookData(BorrowBook borrowBook);
    Task<bool> CheckDuplicateBorrowBookData(int bookId, int borrowerId);
}