using Common.Exceptions;
using Common.Models.Grading;
namespace Repositories.Tests.Repositories.GradingResultRepositoryTests;

public sealed class UpdateAsync: Infrastructure
{
    private GradingResult gradingResult = null!;

    //
    [Fact]
    public async Task ChangesPersist()
    {
        // Arrange
        this.gradingResult = this.Fixture.Build<GradingResult>().Without(e=>e.GradedSubmission).Create();
        this.Context.GradingResults.Add(this.gradingResult);
        this.Context.SaveChanges(); 
        
        // Act
        this.gradingResult.Comment = "new comments";
        await this.CallAsync();
        
        // Assert
        var result =
            this.Context.GradingResults.FirstOrDefault();
            result.Comment.Should().Be("new comments");
    }
    
    [Fact]
    public async Task ThrowsEntityNotFoundException()
    {
        // Arrange
        this.gradingResult = this.Fixture.Build<GradingResult>().Without(e=>e.GradedSubmission).Create();
        this.gradingResult.Id = Guid.NewGuid();
        
        // Act
        Func<Task> act = async () => await this.CallAsync();
        
        // Assert
        await act.Should().ThrowAsync<EntityNotFoundException<GradingResult>>();
    }

    private  Task CallAsync()
    {
        return this.Repository.UpdateAsync(this.gradingResult, default);
    }
}