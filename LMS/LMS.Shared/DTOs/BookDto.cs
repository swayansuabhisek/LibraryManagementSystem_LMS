namespace LMS.Shared.DTOs;

public class BookDto
{
    public int BookNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public bool IsAvailable { get; set; }
}
