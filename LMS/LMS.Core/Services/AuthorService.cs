using LMS.Infrastructure.Repositories.Interface;
using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;

namespace LMS.Service.Services;

public class AuthorService: IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAuthors();
        var authorDtos = authors.Select(a => new AuthorDto()
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description
        }).ToList();
        return authorDtos;
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        var author = await _authorRepository.GetAuthorById(id);
        var authorDto = author == null ? null : new AuthorDto()
        {
            Id = author.Id,
            Name = author.Name,
            Description = author.Description
        };
        return authorDto;
    }

    public async Task AddAuthorAsync(CreateAuthorDto author)
    {
        var authorEntity = new Author()
        {
            Name = author.Name,
            Description = author.Description
        };
        await _authorRepository.AddAuthor(authorEntity);
    }

    public async Task UpdateAuthorAsync(UpdateAuthorDto author)
    {
        var authorEntity = new Author()
        {
            Id = author.Id,
            Name = author.Name,
            Description = author.Description
        };
        await _authorRepository.UpdateAuthor(authorEntity);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        await _authorRepository.DeleteAuthor(id);
    }
}