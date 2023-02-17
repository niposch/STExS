using Common.Models.Grading;
namespace Repositories.Tests.Repositories.GradingResultRepositoryTests;

public sealed class GetBySubmissionIdAsync : Infrastructure
{
    [Fact]
    public async Task ReturnsNullWhenSubmissionIdDoesNotExist()
    {
        // Act
        var gradingResult = await this.Repository.GetBySubmissionIdAsync(Guid.NewGuid());

        // Assert
        gradingResult.Should().BeNull();
    }

    [Fact]
    public async Task ReturnsTheCorrectEntity()
    {
        // Arrange
        var expected = this.Fixture.Build<GradingResult>()
            .With(x=>x.UserSubmissionId)
            .Create();
        this.Context.GradingResults.Add(expected);
        this.Context.GradingResults.Add(this.Fixture.Create<GradingResult>());
        this.Context.SaveChanges();

        // Act
        var res = await this.Repository.GetBySubmissionIdAsync(expected.UserSubmissionId);

        // Assert
        res.Should().Be(expected);
    }
}