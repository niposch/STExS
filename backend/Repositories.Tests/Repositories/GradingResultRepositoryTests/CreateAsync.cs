using Common.Exceptions;
using Common.Models.Grading;

namespace Repositories.Tests.Repositories.GradingResultRepositoryTests;

public sealed class CreateAsync : Infrastructure
{
    private GradingResult? gradingResult;

    //
    [Fact]
    public async Task CreateGradingResult()
    {
        // Arrange
        this.gradingResult = this.Fixture.Build<GradingResult>()
            .With(e => e.Comment, "this is comment")
            .Without(e => e.GradedSubmission).Create();

        // Act
        await this.CallAsync();

        // Assert
        var result =
            this.Context.GradingResults.FirstOrDefault();
        result.Comment.Should().Be("this is comment");
    }

    private Task CallAsync()
    {
        return this.Repository.CreateAsync(this.gradingResult, default);
    }
}