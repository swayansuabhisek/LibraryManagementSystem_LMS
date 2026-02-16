namespace LMS.Shared.DTOs;

public class UpdateBookDto
{
    public int Number { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public int TotalCopies { get; set; }
}
