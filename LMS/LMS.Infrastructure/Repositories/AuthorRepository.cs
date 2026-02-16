using LMS.Infrastructure.Repositories.Interface;
using LMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _dbContext;
    public AuthorRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Author>> GetAllAuthors()
    {
        return await _dbContext.Authors.ToListAsync();
    }

    public async Task<Author?> GetAuthorById(int id)
    {
        return await _dbContext.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAuthor(Author author)
    {
        await _dbContext.Authors.AddAsync(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAuthor(Author author)
    {
        _dbContext.Authors.Update(author);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAuthor(int id)
    {
        var existingAuthor = await _dbContext.Authors.FindAsync(id);
        if (existingAuthor == null)
        {
            throw new ArgumentException("Author not found");
        }
        _dbContext.Authors.Remove(existingAuthor);
        await _dbContext.SaveChangesAsync();
    }
}