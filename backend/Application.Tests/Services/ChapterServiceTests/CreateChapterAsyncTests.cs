using Application.Helper.Roles;
using Common.Models.Authentication;
using Common.Models.ExerciseSystem;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.Tests.Services.ChapterServiceTests;

public sealed class CreateChapterAsyncTests : Infrastructure
{
    private string chapterName = null!;
    private string chapterDescription = null!;
    private Guid moduleId;
    private Guid ownerId;

    [Fact]

    public async Task CreatesChapter()
    {
        // Arrange
       this.chapterName = "Test Chapter";
       this.chapterDescription = "Test Chapter Description";
       this.moduleId = Guid.NewGuid();
       this.ownerId = Guid.NewGuid();
       
       this.ApplicationDbContext.Modules.Add(this.Fixture.Build<Module>()
           .With(m => m.Id, this.moduleId)
           .With(m => m.OwnerId, this.ownerId)
           .Create());
       var user = this.Fixture.Build<ApplicationUser>()
           .With(m => m.Id, this.ownerId)
           .Create();
       this.ApplicationDbContext.SaveChanges();
       
       this.UserManagerMock.Setup(m => m.FindByIdAsync(this.ownerId.ToString()))
           .ReturnsAsync(user);
        
       this.UserManagerMock.Setup(m => m.GetRolesAsync(user))
           .ReturnsAsync(new List<string>{RoleHelper.Admin});
       
       // Act
       await this.CallAsync();
       
       // Assert
       var chapter = await this.ApplicationDbContext.Chapters.FirstOrDefaultAsync();
       chapter.Should().NotBeNull();
       chapter!.ChapterName.Should().Be(this.chapterName);
       chapter.ChapterDescription.Should().Be(this.chapterDescription);
       chapter.ModuleId.Should().Be(this.moduleId);
       chapter.OwnerId.Should().Be(this.ownerId);
       
    }

    private Task CallAsync()
    {
        return this.ChapterService.CreateChapterAsync(this.moduleId,
            this.chapterName,
            this.chapterDescription, 
            this.ownerId);
    }
}