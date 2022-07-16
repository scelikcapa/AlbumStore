using AutoMapper;
using FluentAssertions;
using WebApi.Application.AlbumOperations.Commands.UpdateAlbum;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Commands.Update;

public class UpdateAlbumCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateAlbumCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAlbumIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateAlbumCommand(context, mapper);
        command.AlbumId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("AlbumId: " + command.AlbumId + " does not exist.");
    }

    [Fact]
    public void WhenGivenAlbumTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var albumInDb = new Album{ 
                        Title = "WhenGivenAlbumTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};

        var albumUpdating = new Album{ 
                        Title = "WhenGivenAlbumTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};

        context.Albums.Add(albumInDb);
        context.Albums.Add(albumUpdating);
        context.SaveChanges();

        var command = new UpdateAlbumCommand(context,mapper);
        command.AlbumId = albumUpdating.Id;
        command.Model = new UpdateAlbumModel{Title = albumInDb.Title};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("AlbumTitle: "+ command.Model.Title +" already exists, choose another name.");
    }

    [Fact]
    public void WhenGivenAlbumIdExistsInDb_Album_ShouldBeUpdated()
    {
        // Arrange
        var albumInDb = new Album{ 
                        Title = "WhenGivenAlbumIdExistsInDb_Album_ShouldBeUpdated1", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};

        var albumCompared = new Album{ 
                            Title = albumInDb.Title,
                            Year = albumInDb.Year, 
                            Price = albumInDb.Price, 
                            GenreId = albumInDb.GenreId, 
                            ProducerId = albumInDb.ProducerId};

        context.Albums.Add(albumInDb);
        context.SaveChanges();

        var command = new UpdateAlbumCommand(context,mapper);
        command.AlbumId = albumInDb.Id;
        command.Model = new UpdateAlbumModel{
                            Title = "WhenGivenAlbumIdExistsInDb_Album_ShouldBeUpdated2", 
                            Year = 1991, 
                            Price = 30, 
                            GenreId = 2, 
                            ProducerId = 2};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var albumUpdated = context.Albums.SingleOrDefault(b=> b.Id == albumInDb.Id);
        albumUpdated.Should().NotBeNull();
        albumUpdated.Title.Should().NotBe(albumCompared.Title);
        albumUpdated.Year.Should().NotBe(albumCompared.Year);
        albumUpdated.Price.Should().NotBe(albumCompared.Price);
        albumUpdated.GenreId.Should().NotBe(albumCompared.GenreId);
        albumUpdated.ProducerId.Should().NotBe(albumCompared.ProducerId);

    }
}