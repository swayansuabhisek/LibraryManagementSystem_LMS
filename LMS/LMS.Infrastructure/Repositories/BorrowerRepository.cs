using LMS.Infrastructure.Repositories.Interface;
using LMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class BorrowerRepository: IBorrowerRepository
{
    private readonly LibraryDbContext _dbContext;
    public BorrowerRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Borrower>> GetAllBorrowers()
    {
        return await _dbContext.Borrowers.ToListAsync();
    }

    public async Task<Borrower?> GetBorrowerById(int Id)
    {
        return await _dbContext.Borrowers.Where(a => a.Id == Id).FirstOrDefaultAsync();
    }

    public async Task AddBorrower(Borrower borrower)
    {
        await _dbContext.Borrowers.AddAsync(borrower);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBorrower(Borrower borrower)
    {
        _dbContext.Borrowers.Update(borrower);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteBorrower(int id)
    {
        var existingBorrower = await _dbContext.Borrowers.FindAsync(id);
        if (existingBorrower == null)
        {
            throw new Exception("Borrower not found");
        }
        _dbContext.Borrowers.Remove(existingBorrower);
        await _dbContext.SaveChangesAsync();
    }
}