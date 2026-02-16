using FluentAssertions;
using LMS.Shared.Models;
namespace LMS.UnitTest;

public class AuthorTests
{
    [Fact]
    public void Author_Verify_Its_Properties()
    {
        var author = new Author
        {
            Id = 1,
            Name = "Abhisek Author",
            Description = "Engineering Book Writer"
        };
        author.Id.Should().Be(1);
        author.Name.Should().Be("Abhisek Author");
        author.Description.Should().Be("Engineering Book Writer");
    }
}