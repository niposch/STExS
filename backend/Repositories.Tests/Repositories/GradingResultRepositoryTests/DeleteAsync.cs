using Common.Exceptions;
using Common.Models.Grading;

namespace Repositories.Tests.Repositories.GradingResultRepositoryTests;

public sealed class DeleteAsync : Infrastructure
{
    private GradingResult gradingResult;

    [Fact]
    public async Task DeleteGradingResult()
    {
        // Arrange
        this.gradingResult = this.Fixture.Build<GradingResult>()
            .With(e => e.Comment, "this is comment")
            .Without(e => e.GradedSubmission)
            .Create();
        this.Context.GradingResults.Add(this.gradingResult);
        this.Context.SaveChanges();
        // Act
        await this.CallAsync();
        // Assert
        var result = this.Context.GradingResults.FirstOrDefault(e => e.Id == this.gradingResult.Id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task ThrowsEntityNotFoundException()
    {
        // Arrange
        this.gradingResult = this.Fixture.Build<GradingResult>().Without(e => e.GradedSubmission).Create();

        // Act
        Func<Task> act = async () => await this.CallAsync();

        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<GradingResult>>();
    }

    private Task CallAsync()
    {
        return this.Repository.DeleteAsync(this.gradingResult, default);
    }
}