namespace LMS.Shared.DTOs;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public int TotalCopies { get; set; }
}
