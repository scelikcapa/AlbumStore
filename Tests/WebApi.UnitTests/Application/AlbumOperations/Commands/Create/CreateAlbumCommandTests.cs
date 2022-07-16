using AutoMapper;
using FluentAssertions;
using WebApi.Application.AlbumOperations.Commands.CreateAlbum;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Commands.Create;

public class CreateAlbumCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public CreateAlbumCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAlbumTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var albumInDb = new Album{ 
                        Title = "WhenGivenAlbumTitleAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};

        context.Albums.Add(albumInDb);
        context.SaveChanges();

        var command = new CreateAlbumCommand(context,mapper);
        command.Model = new CreateAlbumModel{Title = albumInDb.Title};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("AlbumTitle: " + command.Model.Title + " already exists, choose another name.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Album_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateAlbumCommand(context,mapper);
        command.Model = new CreateAlbumModel{
                            Title = "WhenValidInputsAreGiven_Album_ShouldBeCreated", 
                            Year = 1991, 
                            Price = 30, 
                            GenreId = 2, 
                            ProducerId = 2};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var albumCreated = context.Albums.SingleOrDefault(b=> b.Title == command.Model.Title);
        albumCreated.Should().NotBeNull();
        albumCreated.Title.Should().Be(command.Model.Title);
        albumCreated.Year.Year.Should().Be(command.Model.Year);
        albumCreated.Price.Should().Be(command.Model.Price);
        albumCreated.GenreId.Should().Be(command.Model.GenreId);
        albumCreated.ProducerId.Should().Be(command.Model.ProducerId);

    }
}