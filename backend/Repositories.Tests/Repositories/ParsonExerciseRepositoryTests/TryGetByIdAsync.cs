using Common.Models.ExerciseSystem.Parson;
namespace Repositories.Tests.Repositories.ParsonExerciseRepositoryTests;

public sealed class TryGetByIdAsync: Infrastructure
{
    [Fact]
    public async Task ReturnsNullWhenParsonExerciseDoesNotExist()
    {
        // Act
        var parsonExercise = await this.Repository.TryGetByIdAsync(Guid.NewGuid());

        // Assert
        parsonExercise.Should().BeNull();
    }

    [Fact]
    public async Task ReturnsTheCorrectEntity()
    {
        // Arrange
        var expected = this.Fixture.Create<ParsonExercise>();
        this.Context.ParsonExercises.Add(expected);
        this.Context.ParsonExercises.Add(this.Fixture.Create<ParsonExercise>());
        this.Context.SaveChanges();
        
        // Act
        var res = await this.Repository.TryGetByIdAsync(expected.Id);
        
        // Assert
        res.Should().Be(expected);
    }
}