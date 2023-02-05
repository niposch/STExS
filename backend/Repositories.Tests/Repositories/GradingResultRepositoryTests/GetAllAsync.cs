using Common.Exceptions;
using Common.Models.Grading;

namespace Repositories.Tests.Repositories.GradingResultRepositoryTests;

public sealed class GetAllAsync : Infrastructure
{
    private Guid userId;

    private GradingResult gradingResult_1;

    private GradingResult gradingResult_2;

    [Fact]
    public async Task QueriesGradingResults()
    {
        // Arrange
        this.userId = new Guid();
        this.gradingResult_1 = this.Fixture.Build<GradingResult>()
            .With(e => e.Comment, "comment 1")
            .With(e => e.UserSubmission)
            .Without(e => e.GradedSubmission)
            .Create();
        this.gradingResult_2 = this.Fixture.Build<GradingResult>()
            .With(e => e.Comment, "comment 2")
            .With(e => e.UserSubmission)
            .Without(e => e.GradedSubmission)
            .Create();
        this.gradingResult_1.UserSubmission.UserId = this.userId;
        this.gradingResult_2.UserSubmission.UserId = this.userId;

        this.Context.GradingResults.AddRange(this.gradingResult_1, this.gradingResult_2);
        this.Context.SaveChanges();

        // Act
        var res = (await this.CallAsync()).ToList();
        // Assert
        res.Should().NotBeNull();
        res.Should().HaveCount(2);
    }

    [Fact]
    public async Task ShouldReturnEmptyList()
    {
        // Arrange

        // Act
        var res = (await this.CallAsync()).ToList();

        // Assert

        res.Should().NotBeNull();
        res.Should().BeEmpty();
    }

    private Task<List<GradingResult>> CallAsync()
    {
        return this.Repository.GetAllAsync(this.userId, default);
    }
}