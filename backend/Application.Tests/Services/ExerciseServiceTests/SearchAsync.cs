using Common.Models.ExerciseSystem.CodeOutput;

namespace Application.Tests.Services.ExerciseServiceTests;

public sealed class SearchAsync : Infrastructure
{
    [Fact]
    public async Task ReturnsAllExercises()
    {
        // Arrange
        var exercises = this.Fixture.Build<CodeOutputExercise>()
            .CreateMany(10)
            .ToList();
        this.ApplicationDbContext.Exercises.AddRange(exercises);
        this.ApplicationDbContext.SaveChanges();

        // Act
        var result = await this.ExerciseService.SearchAsync("");

        // Assert
        result.Should().HaveCount(10);
    }

    [Fact]
    public async Task ReturnsAllExercisesWithSearch()
    {
        // Arrange
        var ignored = this.Fixture.Build<CodeOutputExercise>()
            .CreateMany(10)
            .ToList();
        var expectToBeFound = this.Fixture.Build<CodeOutputExercise>()
            .With(e => e.Description, "blah blah Exercise blah suche blah")
            .CreateMany(10)
            .ToList();
        
        this.ApplicationDbContext.Exercises.AddRange(ignored);
        this.ApplicationDbContext.Exercises.AddRange(expectToBeFound);
        
        this.ApplicationDbContext.SaveChanges();
        
        // Act
        var result = await this.ExerciseService.SearchAsync("exercise suche");

        // Assert
        result.Should().HaveCount(10);
    }
}