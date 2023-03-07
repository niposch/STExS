using Common.Exceptions;
using Common.Models.ExerciseSystem.Cloze;
using Common.Models.Grading;

namespace Application.Tests.Services.ClozeTextExerciseServiceTests;

public sealed class GetByIdAsync : Infrastructure
{
    [Fact]
    public async Task ShouldReturnExercise_WhenExists()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var exercise = this.Fixture.Build<ClozeTextExercise>()
            .With(m => m.TextWithAnswers, "This is a [[test]] text.")
            .Without(m => m.UserSubmissions)
            .With(m => m.Id, exerciseId)
            .With(m => m.OwnerId, userId)
            .Create();
        await this.Context.ClozeExercises.AddAsync(exercise);
        await this.Context.SaveChangesAsync();

        // Act
        var result = await this.ClozeTextExerciseService.GetByIdAsync(exerciseId,
            Guid.NewGuid(),
            false);

        // Assert
        Assert.Equal(exerciseId, result.Id);
    }

    [Fact]
    public async Task ShouldThrowException_WhenExerciseDoesNotExist()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();

        // Act
        var act = () =>
            this.ClozeTextExerciseService.GetByIdAsync(exerciseId,
                Guid.NewGuid(),
                false);

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException<ClozeTextExercise>>(act);
    }

    [Fact]
    public async Task UserSolvedExercise_ShouldBeTrue_WhenUserSolvedExercise()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var exercise = this.Fixture.Build<ClozeTextExercise>()
            .With(m => m.TextWithAnswers, "This is a [[test]] text.")
            .Without(m => m.UserSubmissions)
            .With(m => m.Id, exerciseId)
            .With(m => m.OwnerId, userId)
            .Create();

        var userSubmission = this.Fixture.Build<UserSubmission>()
            .With(m => m.UserId, userId)
            .With(m => m.ExerciseId, exerciseId)
            .With(m => m.FinalSubmissionId, Guid.NewGuid())
            .Without(m => m.Exercise)
            .Without(m => m.User)
            .With(m => m.FinalSubmission, this.Fixture
                .Build<ClozeTextSubmission>()
                .Without(m => m.UserSubmission)
                .Without(m => m.GradingResult)
                .With(m => m.UserId, userId)
                .With(m => m.ExerciseId, exerciseId)
                .Create())
            .Create();
        
        userSubmission.FinalSubmissionId = userSubmission.FinalSubmission!.Id;

        await this.Context.ClozeExercises.AddAsync(exercise);
        await this.Context.UserSubmissions.AddAsync(userSubmission);
        await this.Context.SaveChangesAsync();

        // Act
        var result = await this.ClozeTextExerciseService.GetByIdAsync(exerciseId,
            userId,
            false);

        // Assert
        result.UserHasSolvedExercise.Should().BeTrue();
    }

    [Fact]
    public async Task UserSolvedExercise_ShouldBeFalse_WhenUserDidNotSolveExercise()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var exercise = this.Fixture.Build<ClozeTextExercise>()
            .With(m => m.TextWithAnswers, "This is a [[test]] text.")
            .Without(m => m.UserSubmissions)
            .With(m => m.Id, exerciseId)
            .With(m => m.OwnerId, userId)
            .Create();

        var userSubmission = this.Fixture.Build<UserSubmission>()
            .With(m => m.UserId, userId)
            .With(m => m.ExerciseId, exerciseId)
            .Without(m => m.FinalSubmissionId)
            .Without(m => m.FinalSubmission)
            .Without(m => m.Exercise)
            .Create();

        await this.Context.ClozeExercises.AddAsync(exercise);
        await this.Context.UserSubmissions.AddAsync(userSubmission);
        await this.Context.SaveChangesAsync();

        // Act
        var result = await this.ClozeTextExerciseService.GetByIdAsync(exerciseId,
            userId,
            false);

        // Assert
        result.UserHasSolvedExercise.Should().BeFalse();
    }

    [Fact]
    public async Task ShouldReturnExerciseWithAnswers_WhenWithAnswersIsTrue()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var exercise = this.Fixture.Build<ClozeTextExercise>()
            .With(m => m.TextWithAnswers, "This is a [[test]] text. How would it handle [[multiple]] answers?")
            .Without(m => m.UserSubmissions)
            .With(m => m.Id, exerciseId)
            .With(m => m.OwnerId, userId)
            .Create();

        await this.Context.ClozeExercises.AddAsync(exercise);
        await this.Context.SaveChangesAsync();

        // Act
        var result = await this.ClozeTextExerciseService.GetByIdAsync(exerciseId,
            Guid.NewGuid(),
            true);

        // Assert
        result.Text.Should().Be("This is a [[test]] text. How would it handle [[multiple]] answers?");
    }

    [Fact]
    public async Task ShouldReturnExerciseWithoutAnswers_WhenWithAnswersIsFalse()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var exercise = this.Fixture.Build<ClozeTextExercise>()
            .With(m => m.TextWithAnswers, "This is a [[test]] text. How would it handle [[multiple]] answers?")
            .Without(m => m.UserSubmissions)
            .With(m => m.Id, exerciseId)
            .With(m => m.OwnerId, userId)
            .Create();

        await this.Context.ClozeExercises.AddAsync(exercise);
        await this.Context.SaveChangesAsync();

        // Act
        var result = await this.ClozeTextExerciseService.GetByIdAsync(exerciseId,
            Guid.NewGuid(),
            false);

        // Assert
        result.Text.Should().Be("This is a [[]] text. How would it handle [[]] answers?");
    }
}