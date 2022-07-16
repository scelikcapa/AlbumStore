using FluentAssertions;
using WebApi.Application.SingerOperations.Commands.DeleteSinger;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Commands.Delete;

public class DeleteSingerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;

    public DeleteSingerCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenSingerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteSingerCommand(context);
        command.SingerId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("SingerId: " + command.SingerId + " does not exists.");
    }

    [Fact]
    public void WhenGivenSingerHasRelation_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var albumInDb = new Album{
                        Title = "WhenGivenSingerHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};

        context.Albums.Add(albumInDb);                
        context.SaveChanges();

        var singerInDb = new Singer{
                        Name = "WhenGivenSingerHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenSingerHasRelation_InvalidOperationException_ShouldBeReturn",
                        Albums = new List<Album>{albumInDb}};

        context.Singers.Add(singerInDb);
        context.SaveChanges();

        var command = new DeleteSingerCommand(context);
        command.SingerId = singerInDb.Id;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("SingerId: " + command.SingerId + " has " +singerInDb.Albums.Count()+ " albums. Please delete them first.");
    }

    [Fact]
    public void WhenGivenSingerHasNotRelation_Singer_ShouldBeDeleted()
    {
        // Arrange
        var singerInDb = new Singer{
                        Name = "WhenGivenSingerHasNotRelation_Singer_ShouldBeDeleted", 
                        Surname = "WhenGivenSingerHasNotRelation_Singer_ShouldBeDeleted",
                        Albums = null};

        context.Singers.Add(singerInDb);
        context.SaveChanges();

        var command = new DeleteSingerCommand(context);
        command.SingerId = singerInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var singer = context.Singers.SingleOrDefault(b=> b.Id == command.SingerId);
        singer.Should().BeNull();
    }
}