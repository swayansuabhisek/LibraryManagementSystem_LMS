using LMS.Shared.Models;

namespace LMS.Infrastructure.Repositories.Interface;

public interface IBorrowerRepository
{
    Task<List<Borrower>> GetAllBorrowers();
    Task<Borrower?> GetBorrowerById(int id);
    Task AddBorrower(Borrower borrower);
    Task UpdateBorrower(Borrower borrower);
    Task DeleteBorrower(int id);
}