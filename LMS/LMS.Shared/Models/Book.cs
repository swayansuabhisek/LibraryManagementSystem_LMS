namespace LMS.Shared.Models;

public class Book
{
    public int BookNumber { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int AvailableCopies { get; set; }
    public int TotalCopies { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public bool IsAvailable { get; set; }
    
    public ICollection<BorrowBook> BorrowBooks { get; set; } = new List<BorrowBook>();
}