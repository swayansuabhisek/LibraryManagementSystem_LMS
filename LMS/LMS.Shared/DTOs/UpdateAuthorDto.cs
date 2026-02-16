namespace LMS.Shared.DTOs;

public class UpdateAuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}