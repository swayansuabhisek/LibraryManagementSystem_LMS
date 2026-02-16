using LMS.Shared.DTOs;
using LMS.Shared.Models;

namespace LMS.Core.Services.Interface;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task AddAuthorAsync(CreateAuthorDto author);
    Task UpdateAuthorAsync(UpdateAuthorDto author);
    Task DeleteAuthorAsync(int id);
}