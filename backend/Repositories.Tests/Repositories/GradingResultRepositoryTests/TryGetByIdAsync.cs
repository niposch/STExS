using Common.Models.Grading;
namespace Repositories.Tests.Repositories.GradingResultRepositoryTests;

public sealed class TryGetByIdAsync : Infrastructure
{
    [Fact]
    public async Task ReturnsNullWhenGradingResultDoesNotExist()
    {
        // Act
        var gradingResult = await this.Repository.TryGetByIdAsync(Guid.NewGuid());

        // Assert
        gradingResult.Should().BeNull();
    }

    [Fact]
    public async Task ReturnsTheCorrectEntity()
    {
        // Arrange
        var expected = this.Fixture.Create<GradingResult>();
        this.Context.GradingResults.Add(expected);
        this.Context.GradingResults.Add(this.Fixture.Create<GradingResult>());
        this.Context.SaveChanges();

        // Act
        var res = await this.Repository.TryGetByIdAsync(expected.Id);

        // Assert
        res.Should().Be(expected);
    }
}