using Common.Exceptions;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Parson;

namespace Application.Tests.Services.ParsonExerciseServiceTests;

public class GetByIdAsync : Infrastructure
{
    [Fact]
    public async Task ShouldThrowWhenNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        Func<Task> action = async () => await this.Service.GetByIdAsync(id, Guid.NewGuid());

        // Assert
        await action.Should().ThrowAsync<EntityNotFoundException<ParsonExercise>>();
    }

    [Fact]
    public async Task ShouldReturnCorrectly()
    {
        // Arrange
        var parsonExercise = this.Fixture.Build<ParsonExercise>()
            .With(m => m.ExerciseType, ExerciseType.Parson)
            .With(m => m.ExpectedSolution, this.Fixture.Build<ParsonSolution>()
                .Without(s => s.CodeElements)
                .Create())
            .Without(m => m.UserSubmissions)
            .Create();

        parsonExercise.ExpectedSolution.CodeElements = new List<ParsonElement>
        {
            new()
            {
                Code = "Line 1",
                Indentation = 1
            },
            new()
            {
                Code = "Line 2",
                Indentation = 2
            }
        };

        var userId = Guid.NewGuid();
        this.Context.ParsonExercises.Add(parsonExercise);
        this.Context.SaveChanges();


        // Act
        var result = await this.Service.GetByIdAsync(parsonExercise.Id, userId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(parsonExercise.Id);
        result.ExerciseName.Should().Be(parsonExercise.ExerciseName);
        result.ChapterId.Should().Be(parsonExercise.ChapterId);
        result.ExerciseDescription.Should().Be(parsonExercise.Description);
        result.Lines.Should().HaveCount(2);
        result.Lines.Select(l => l.Indentation).Should().OnlyContain(i => i == 0);
        result.Lines.Should().ContainSingle(l => l.Text == "Line 1");
        result.Lines.Should().ContainSingle(l => l.Text == "Line 2");
        result.AchievablePoints.Should().Be(parsonExercise.AchievablePoints);
        result.ExerciseType.Should().Be(ExerciseType.Parson);
    }
}