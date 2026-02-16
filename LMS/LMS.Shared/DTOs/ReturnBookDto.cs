namespace LMS.Shared.DTOs;

public class ReturnBookDto
{
    public int BookId { get; set; }
    public int BorrowerId { get; set; }
    public DateTime ReturnDate { get; set; }
}