using Application.DTOs.ExercisesDTOs.ClozeText;

namespace Application.Tests.Services.ClozeTextExerciseServiceTests;

public sealed class CreateAsync : Infrastructure
{
    [Fact]
    public async Task ShouldCreateExercise_WhenValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var createItem = this.Fixture.Build<ClozeTextExerciseCreateItem>()
            .With(m => m.Text, "This is a [[test]] text.")
            .Create();

        // Act
        var result = await this.ClozeTextExerciseService.CreateAsync(createItem, userId);

        // Assert
        var entity = await this.Context.ClozeExercises.FindAsync(result);
        entity.TextWithAnswers.Should().Be("This is a [[test]] text.");
    }
}