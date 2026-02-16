using LMS.Infrastructure.Repositories.Interface;
using LMS.Core.Services.Interface;
using LMS.Shared.DTOs;
using LMS.Shared.Models;

namespace LMS.Service.Services;

public class BorrowerService: IBorrowerService
{
    private readonly IBorrowerRepository _borrowerRepository;

    public BorrowerService(IBorrowerRepository borrowerRepository)
    {
        _borrowerRepository = borrowerRepository;
    }
    public async Task<List<BorrowerDto>> GetAllBorrowersAsync()
    {
        var borrowers = await _borrowerRepository.GetAllBorrowers();
        var borrowerDtos = borrowers.Select(borrower => new BorrowerDto()
        {
            Id = borrower.Id,
            FullName = borrower.Name,
            Email = borrower.Email,
            PhoneNumber = borrower.PhoneNumber
        }).ToList();
        return borrowerDtos;
    }

    public async Task<BorrowerDto?> GetBorrowerByIdAsync(int id)
    {
        var  borrower = await _borrowerRepository.GetBorrowerById(id);
        var borrowerDto = borrower == null? null : new BorrowerDto()
        {
            Id = borrower.Id,
            FullName = borrower.Name,
            Email = borrower.Email,
            PhoneNumber = borrower.PhoneNumber
        };
        return borrowerDto;
    }

    public async Task AddBorrowerAsync(CreateBorrowerDto borrower)
    {
        var borrowerEntity = new Borrower()
        {
            Name = borrower.Name,
            Email = borrower.Email,
            PhoneNumber = borrower.PhoneNumber
        };
        await _borrowerRepository.AddBorrower(borrowerEntity);
    }

    public async Task UpdateBorrowerAsync(UpdateBorrowerDto borrower)
    {
        var borrowerEntity = new Borrower()
        {
            Id = borrower.Id,
            Name = borrower.Name,
            Email = borrower.Email,
            PhoneNumber = borrower.PhoneNumber
        };
        await _borrowerRepository.UpdateBorrower(borrowerEntity);
    }

    public async Task DeleteBorrowerAsync(int id)
    {
        await _borrowerRepository.DeleteBorrower(id);
    }
}