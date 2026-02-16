namespace LMS.Shared.DTOs;

public class BorrowBookDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int BorrowerId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public DateTime IssuedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; }
}