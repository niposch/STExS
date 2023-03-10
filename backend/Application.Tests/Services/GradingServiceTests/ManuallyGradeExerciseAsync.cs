using Common.Exceptions;
using Common.Models.ExerciseSystem.CodeOutput;
using Common.Models.Grading;
using Moq;

namespace Application.Tests.Services.GradingServiceTests;

public sealed class ManuallyGradeExerciseAsync: Infrastructure
{
    [Fact]
    public async Task ThrowsUnauthorizedAccessException_WhenUserIsNotModuleAdmin()
    {
        // Arrange
        var submission = this.Fixture.Build<CodeOutputSubmission>()
            .Without(s => s.ExerciseId)
            .Create();
        var newGrade = this.Fixture.Create<int>();
        var comment = this.Fixture.Create<string>();
        var changedByUserId = Guid.NewGuid();
        this.Context.Submissions.Add(submission);
        await this.Context.SaveChangesAsync();
        this.AccessServiceMock.Setup(s => s.IsModuleAdmin(
            It.Is<Guid>(c => c == submission.UserSubmission.Exercise.Chapter.Module.Id),
            It.Is<Guid>(c => c == changedByUserId),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        

        // Act
        var act = async () => await this.Service.ManuallyGradeExerciseAsync(submission.Id, newGrade, comment, changedByUserId);
        
        // Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
    }
    
    [Fact]
    public async Task ThrowsEntityNotFoundException_WhenSubmissionDoesNotExist()
    {
        // Arrange
        var submissionId = this.Fixture.Create<Guid>();
        var newGrade = this.Fixture.Create<int>();
        var comment = this.Fixture.Create<string>();
        var changedByUserId = this.Fixture.Create<Guid>();

        // Act
        var act = async () => await this.Service.ManuallyGradeExerciseAsync(submissionId, newGrade, comment, changedByUserId);
        
        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException<BaseSubmission>>(act);
    }
    
    [Fact]
    public async Task CreatesGradingResult_WhenSubmissionDoesNotHaveOne()
    {
        // Arrange
        var submission = this.Fixture.Build<CodeOutputSubmission>()
            .Without(s => s.ExerciseId)
            .Without(s => s.GradingResult)
            .Create();
        var newGrade = this.Fixture.Create<int>();
        var comment = this.Fixture.Create<string>();
        var changedByUserId = Guid.NewGuid();
        this.Context.Submissions.Add(submission);
        await this.Context.SaveChangesAsync();
        this.AccessServiceMock.Setup(s => s.IsExerciseAdminAsync(
            It.Is<Guid>(c => c == submission.UserSubmission.Exercise.Id),
            It.Is<Guid>(c => c == changedByUserId),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        // Act
        await this.Service.ManuallyGradeExerciseAsync(submission.Id, newGrade, comment, changedByUserId);
        
        // Assert
        var result = this.Context.Submissions.Find(submission.Id);
        result.GradingResult.Should().NotBeNull();
        result.GradingResult.GradingState.Should().Be(GradingState.FinallyManuallyGraded);
        result.GradingResult.Points.Should().Be(newGrade);
        result.GradingResult.Comment.Should().Be(comment);
        result.GradingResult.ManualGradingDate.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 1, 0));
        result.GradingResult.IsAutomaticallyGraded.Should().BeFalse();
        result.GradingResult.AppealableBefore.Should().BeNull();
    }

    [Fact]
    public async Task ShouldUpdateExistingManualGradingResult()
    {
        // Arrange
        var submission = this.Fixture.Build<CodeOutputSubmission>()
            .Without(s => s.ExerciseId)
            .Create();
        var newGrade = this.Fixture.Create<int>();
        var comment = this.Fixture.Create<string>();
        var changedByUserId = Guid.NewGuid();
        this.Context.Submissions.Add(submission);
        await this.Context.SaveChangesAsync();
        this.AccessServiceMock.Setup(s => s.IsExerciseAdminAsync(
            It.Is<Guid>(c => c == submission.UserSubmission.Exercise.Id),
            It.Is<Guid>(c => c == changedByUserId),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        // Act
        await this.Service.ManuallyGradeExerciseAsync(submission.Id, newGrade, comment, changedByUserId);
        
        // Assert
        var result = this.Context.Submissions.Find(submission.Id);
        result.GradingResult.Should().NotBeNull();
        result.GradingResult.GradingState.Should().Be(GradingState.FinallyManuallyGraded);
        result.GradingResult.Points.Should().Be(newGrade);
        result.GradingResult.Comment.Should().Be(comment);
        result.GradingResult.ManualGradingDate.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 1, 0));
        result.GradingResult.IsAutomaticallyGraded.Should().BeFalse();
        result.GradingResult.AppealableBefore.Should().BeNull();
    }
}