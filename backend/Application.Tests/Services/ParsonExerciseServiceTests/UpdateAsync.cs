using Application.DTOs.ExercisesDTOs.Parson;
using Application.Services.Interfaces;
using Common.Models.ExerciseSystem;
using Common.Models.ExerciseSystem.Parson;

namespace Application.Tests.Services.ParsonExerciseServiceTests;

public class UpdateAsync : Infrastructure
{
    [Fact]
    public async Task ShouldUpdateCommonFields()
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

        this.Context.ParsonExercises.Add(parsonExercise);
        this.Context.SaveChanges();

        var updateItem = this.Fixture.Build<ParsonExerciseDetailItemWithAnswer>()
            .With(m => m.Id, parsonExercise.Id)
            .With(m => m.ExerciseType, ExerciseType.Parson)
            .Create();

        updateItem.Lines = new List<ParsonExerciseLineDetailItem>
        {
            new()
            {
                Text = "Line 1",
                Indentation = 1,
                Id = parsonExercise.ExpectedSolution.CodeElements[0].Id
            },
            new()
            {
                Text = "Line 2",
                Indentation = 2,
                Id = parsonExercise.ExpectedSolution.CodeElements[1].Id
            }
        };

        // Act
        await this.Service.UpdateAsync(updateItem);

        // Assert
        var result = this.Context.ParsonExercises.First(m => m.Id == parsonExercise.Id);
        result.Should().NotBeNull();
        result.Id.Should().Be(parsonExercise.Id);
        result.ExerciseName.Should().Be(updateItem.ExerciseName);
        result.ChapterId.Should().Be(updateItem.ChapterId);
        result.ExerciseType.Should().Be(updateItem.ExerciseType);
        result.ExpectedSolution.Should().NotBeNull();
        result.ExpectedSolution.CodeElements.Should().NotBeNull();
        result.ExpectedSolution.CodeElements.Count.Should().Be(2);
        result.ExpectedSolution.CodeElements[0].Code.Should().Be("Line 1");
        result.ExpectedSolution.CodeElements[0].Indentation.Should().Be(1);
        result.ExpectedSolution.CodeElements[1].Code.Should().Be("Line 2");
        result.ExpectedSolution.CodeElements[1].Indentation.Should().Be(2);
    }


    [Fact]
    public async Task ShouldUpdateSolutions()
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
                Indentation = 1,
                RelatedSolutionId = parsonExercise.ExpectedSolution.Id
            },
            new()
            {
                Code = "Line 2",
                Indentation = 2,
                RelatedSolutionId = parsonExercise.ExpectedSolution.Id
            },
            new()
            {
                Code = "Line 3",
                Indentation = 3,
                RelatedSolutionId = parsonExercise.ExpectedSolution.Id
            }
        };

        this.Context.ParsonExercises.Add(parsonExercise);
        this.Context.SaveChanges();
        this.Context.ChangeTracker.Clear();

        var updateItem = this.Fixture.Build<ParsonExerciseDetailItemWithAnswer>()
            .With(m => m.Id, parsonExercise.Id)
            .With(m => m.ExerciseType, ExerciseType.Parson)
            .Create();

        updateItem.Lines = new List<ParsonExerciseLineDetailItem>
        {
            new()
            {
                Text = "ABC",
                Indentation = 3,
                Id = parsonExercise.ExpectedSolution.CodeElements[0].Id
            },
            new()
            {
                Text = "New Line",
                Indentation = 1,
                Id = Guid.Empty
            },
            new()
            {
                Text = "Line 2",
                Indentation = 2,
                Id = parsonExercise.ExpectedSolution.CodeElements[1].Id
            },
            new()
            {
                Text = "New Line 2",
                Indentation = 22,
                Id = Guid.Empty
            }
        };

        // Act
        await this.Service.UpdateAsync(updateItem);

        // Assert
        var result = this.Context.ParsonExercises.First(m => m.Id == parsonExercise.Id);
        result.Should().NotBeNull();
        result.ExpectedSolution.Should().NotBeNull();
        result.ExpectedSolution.CodeElements.Should().NotBeNull();
        result.ExpectedSolution.CodeElements.Count.Should().Be(4);
        result.ExpectedSolution.CodeElements[0].Code.Should().Be("ABC");
        result.ExpectedSolution.CodeElements[0].Indentation.Should().Be(3);
        result.ExpectedSolution.CodeElements[1].Code.Should().Be("New Line");
        result.ExpectedSolution.CodeElements[1].Indentation.Should().Be(1);
        result.ExpectedSolution.CodeElements[2].Code.Should().Be("Line 2");
        result.ExpectedSolution.CodeElements[2].Indentation.Should().Be(2);
        result.ExpectedSolution.CodeElements[3].Code.Should().Be("New Line 2");
        result.ExpectedSolution.CodeElements[3].Indentation.Should().Be(22);

        this.Context.ParsonElements.Count().Should().Be(4);
    }
}