using LMS.Infrastructure.Repositories.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class BorrowBookRepository: IBorrowBookRepository
{
    private readonly LibraryDbContext _dbContext;
    public BorrowBookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BorrowBook>> GetBorrowBooksAsync()
    {
        return await _dbContext.BorrowBooks
            .Include(b => b.Borrower)
            .Include(b => b.Book)
            .ToListAsync();
    }
    
    public async Task AddBorrowBookData(BorrowBook borrowBook)
    {
        await _dbContext.BorrowBooks.AddAsync(borrowBook);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<BorrowBook?> GetBorrowBookDataById(int id)
    {
        return await _dbContext.BorrowBooks
            .Where(b => b.Id == id)
            .Include(b => b.Borrower)
            .Include(b => b.Book)
            .FirstOrDefaultAsync();
    }
    
    public async Task<BorrowBook?> GetBorrowBookDataByBookAndBorrowerId(int bookId, int borrowerId)
    {
        return await _dbContext.BorrowBooks
            .Where(b => b.BookId == bookId && b.BorrowerId == borrowerId)
            .Include(b => b.Borrower)
            .Include(b => b.Book)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateBorrowBookData(BorrowBook borrowBook)
    {
        var existingBorrowBook = await _dbContext.BorrowBooks.FindAsync(borrowBook.Id);
        if (existingBorrowBook == null)
        {
            throw new ArgumentException("BorrowBook Record not found");
        }
        _dbContext.BorrowBooks.Update(borrowBook);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckDuplicateBorrowBookData(int bookId, int borrowerId)
    {
        var isExist = await _dbContext.BorrowBooks.AnyAsync(b => b.BookId == bookId && b.BorrowerId == borrowerId);
        return isExist;
    }
}