using AutoMapper;
using FluentAssertions;
using WebApi.Application.AlbumOperations.Queries.GetAlbumById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Queries.GetAlbumById;

public class GetAlbumByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetAlbumByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenAlbumIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var album = new Album{ 
            Title = "WhenGivenAlbumIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Year = new DateTime(1990,01,01), 
            Price = 20, 
            GenreId = 1, 
            ProducerId = 1};
        context.Albums.Add(album);
        context.SaveChanges();
        
        context.Albums.Remove(album);
        context.SaveChanges();

        GetAlbumByIdQuery command = new GetAlbumByIdQuery(context, mapper);
        command.AlbumId= album.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("AlbumId: " + command.AlbumId + " does not exist.");
    }

    [Fact]
    public void WhenGivenAlbumIdDoesExistInDb_Album_ShouldBeReturned()
    {
        // Arrange
        var album = new Album{ 
            Title = "WhenGivenAlbumIdDoesExistInDb_Album_ShouldBeRetuned", 
            Year = new DateTime(1990,01,01), 
            Price = 20, 
            GenreId = 1, 
            ProducerId = 1};
        context.Albums.Add(album);
        context.SaveChanges();

        GetAlbumByIdQuery command = new GetAlbumByIdQuery(context, mapper);
        command.AlbumId = album.Id;

        // Act
        var albumReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        albumReturned.Should().NotBeNull();
        albumReturned.Id.Should().Be(command.AlbumId);
        albumReturned.Title.Should().Be(album.Title);
        albumReturned.Year.Should().Be(album.Year.Year);
        albumReturned.Price.Should().Be(album.Price);
        albumReturned.GenreId.Should().Be(album.GenreId);
        albumReturned.ProducerId.Should().Be(album.ProducerId);
    }
}