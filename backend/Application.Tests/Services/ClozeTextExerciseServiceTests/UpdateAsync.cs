using Application.DTOs.ExercisesDTOs.ClozeText;
using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Cloze;

namespace Application.Tests.Services.ClozeTextExerciseServiceTests;

public sealed class UpdateAsync : Infrastructure
{
    [Fact]
    public async Task ShouldUpdateExercise_WhenExists()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var exercise = this.Fixture.Build<ClozeTextExercise>()
            .With(m => m.TextWithAnswers, "This is a [[test]] text.")
            .Without(m => m.UserSubmissions)
            .With(m => m.Id, exerciseId)
            .With(m => m.ExerciseType, ExerciseType.ClozeText)
            .With(m => m.OwnerId, userId)
            .Create();
        await this.Context.ClozeExercises.AddAsync(exercise);
        await this.Context.SaveChangesAsync();

        // Act
        var updateItem = new ClozeTextExerciseDetailItem
        {
            Id = exerciseId,
            ExerciseName = "New name",
            ExerciseDescription = "New description",
            AchievablePoints = 10,
            Text = "This is a [[new]] text.",
            RunningNumber = 12,
            ExerciseType = ExerciseType.ProgrammingTask,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            UserHasSolvedExercise = true,
            ChapterId = Guid.NewGuid()
        };

        await this.ClozeTextExerciseService.UpdateAsync(updateItem);

        // Assert
        var result = await this.Context.ClozeExercises.FindAsync(exerciseId);
        result.ExerciseType.Should().Be(ExerciseType.ClozeText);
        result.TextWithAnswers.Should().Be("This is a [[new]] text.");
        result.ExerciseName.Should().Be("New name");
        result.Description.Should().Be("New description");
        result.AchievablePoints.Should().Be(10);
        result.RunningNumber.Should().Be(exercise.RunningNumber);
        result.ChapterId.Should().Be(exercise.ChapterId);
    }

    [Fact]
    public async Task ShouldThrowException_WhenExerciseDoesNotExist()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();

        // Act
        var act = () =>
            this.ClozeTextExerciseService.UpdateAsync(new ClozeTextExerciseDetailItem
            {
                Id = exerciseId,
                ExerciseName = "New name",
                ExerciseDescription = "New description",
                AchievablePoints = 10,
                Text = "This is a [[new]] text.",
                RunningNumber = 12,
                ExerciseType = ExerciseType.ProgrammingTask,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                UserHasSolvedExercise = true,
                ChapterId = Guid.NewGuid()
            });

        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException<ClozeTextExercise>>(act);
    }
}