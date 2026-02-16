using LMS.Shared.Models;

namespace LMS.Infrastructure.Repositories.Interface;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAuthors();
    Task<Author?> GetAuthorById(int id);
    Task AddAuthor(Author author);
    Task UpdateAuthor(Author author);
    Task DeleteAuthor(int id);
}