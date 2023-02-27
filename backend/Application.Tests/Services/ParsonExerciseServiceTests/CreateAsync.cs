using Application.DTOs.ExercisesDTOs.Parson;

namespace Application.Tests.Services.ParsonExerciseServiceTests;

public class CreateAsync : Infrastructure
{
    [Fact]
    public async Task CreateAsync_WhenCalled_ShouldCreateParsonExercise()
    {
        // Arrange
        var parsonExercise = this.Fixture.Build<ParsonExerciseCreateItem>()
            .Without(m => m.Lines)
            .Create();

        parsonExercise.Lines = new List<ParsonExerciseLineCreateItem>
        {
            new()
            {
                Text = "Line 1",
                Indentation = 1
            },
            new()
            {
                Text = "Line 2",
                Indentation = 2
            }
        };

        var userId = Guid.NewGuid();

        // Act
        var id = await this.Service.CreateAsync(parsonExercise, userId);

        // Assert
        this.Context.ParsonExercises.Should().HaveCount(1);
        this.Context.ParsonSolutions.Should().HaveCount(1);
        this.Context.ParsonElements.Should().HaveCount(2);
        var entity = await this.Context.ParsonExercises.FindAsync(id);
        entity.Should().NotBeNull();
        entity.RunningNumber.Should().Be(1);
        entity.ExerciseName.Should().Be(parsonExercise.ExerciseName);
        entity.ChapterId.Should().Be(parsonExercise.ChapterId);
        entity.Description.Should().Be(parsonExercise.ExerciseDescription);
        entity.ExpectedSolution.Should().NotBeNull();
        var lines = entity.ExpectedSolution.CodeElements.OrderBy(l => l.RunningNumber).ToList();
        lines.Should().HaveCount(2);
        lines[0].Code.Should().Be(parsonExercise.Lines[0].Text);
        lines[0].RunningNumber.Should().Be(1);
        lines[0].Indentation.Should().Be(parsonExercise.Lines[0].Indentation);
        lines[1].Code.Should().Be(parsonExercise.Lines[1].Text);
        lines[1].RunningNumber.Should().Be(2);
        lines[1].Indentation.Should().Be(parsonExercise.Lines[1].Indentation);
        lines.Select(l => l.OwnerId).Should().OnlyContain(id => id == userId);
        lines.Select(l => l.RelatedSolutionId).Should().OnlyContain(id => id == entity.ExpectedSolution.Id);
    }

    [Fact]
    public async Task ShouldCreateWithNoLines()
    {
        // Arrange
        var parsonExercise = this.Fixture.Build<ParsonExerciseCreateItem>()
            .Without(m => m.Lines)
            .Create();

        var userId = Guid.NewGuid();

        // Act
        var id = await this.Service.CreateAsync(parsonExercise, userId);

        // Assert
        this.Context.ParsonExercises.Should().HaveCount(1);
        this.Context.ParsonSolutions.Should().HaveCount(1);
        this.Context.ParsonElements.Should().HaveCount(0);
        var entity = await this.Context.ParsonExercises.FindAsync(id);
        entity.Should().NotBeNull();
        entity.RunningNumber.Should().Be(1);
        entity.ExerciseName.Should().Be(parsonExercise.ExerciseName);
        entity.ChapterId.Should().Be(parsonExercise.ChapterId);
        entity.Description.Should().Be(parsonExercise.ExerciseDescription);
        entity.ExpectedSolution.Should().NotBeNull();
        entity.ExpectedSolution.CodeElements.Should().BeEmpty();
    }
}