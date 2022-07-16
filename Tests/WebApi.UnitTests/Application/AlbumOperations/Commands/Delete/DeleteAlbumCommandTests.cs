using FluentAssertions;
using WebApi.Application.AlbumOperations.Commands.DeleteAlbum;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Commands.Delete;

public class DeleteAlbumCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;

    public DeleteAlbumCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAlbumIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteAlbumCommand(context);
        command.AlbumId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Album Id: " + command.AlbumId + " does not exists.");
    }

    [Fact]
    public void WhenGivenAlbumIdExistsInDb_Album_ShouldBeDeleted()
    {
        // Arrange
        var albumInDb = new Album{
                        Title = "WhenGivenAlbumIdExistsInDb_Album_ShouldBeDeleted", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};
                        
        context.Albums.Add(albumInDb);
        context.SaveChanges();

        var command = new DeleteAlbumCommand(context);
        command.AlbumId = albumInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var album = context.Albums.Single(b=> b.Id == command.AlbumId);
        album.IsActive.Should().BeFalse();
    }
}