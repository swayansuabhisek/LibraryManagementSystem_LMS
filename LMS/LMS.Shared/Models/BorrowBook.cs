namespace LMS.Shared.Models;

public class BorrowBook
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int BorrowerId { get; set; }
    public Borrower Borrower { get; set; } = null!;

    public DateTime BookIssueDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public bool IsReturned { get; set; }
}
