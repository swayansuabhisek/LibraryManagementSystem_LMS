namespace LMS.Shared.Models;

public class Borrower
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<BorrowBook> BorrowBooks { get; set; } = new List<BorrowBook>();

}

