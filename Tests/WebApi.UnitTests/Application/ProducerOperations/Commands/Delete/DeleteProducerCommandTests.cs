using FluentAssertions;
using WebApi.Application.ProducerOperations.Commands.DeleteProducer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Commands.Delete;

public class DeleteProducerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;

    public DeleteProducerCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenProducerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteProducerCommand(context);
        command.ProducerId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ProducerId: " + command.ProducerId + " does not exists.");
    }

    [Fact]
    public void WhenGivenProducerHasRelation_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var albumInDb = new Album{
                        Title = "WhenGivenProducerHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Year = new DateTime(1990,01,01), 
                        Price = 20, 
                        GenreId = 1, 
                        ProducerId = 1};

        context.Albums.Add(albumInDb);                
        context.SaveChanges();

        var producerInDb = new Producer{
                        Name = "WhenGivenProducerHasRelation_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenProducerHasRelation_InvalidOperationException_ShouldBeReturn",
                        Albums = new List<Album>{albumInDb}};

        context.Producers.Add(producerInDb);
        context.SaveChanges();

        var command = new DeleteProducerCommand(context);
        command.ProducerId = producerInDb.Id;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ProducerId: " + command.ProducerId + " has " +producerInDb.Albums.Count()+ " albums. Please delete them first.");
    }

    [Fact]
    public void WhenGivenProducerHasNotRelation_Producer_ShouldBeDeleted()
    {
        // Arrange
        var producerInDb = new Producer{
                        Name = "WhenGivenProducerHasNotRelation_Producer_ShouldBeDeleted", 
                        Surname = "WhenGivenProducerHasNotRelation_Producer_ShouldBeDeleted",
                        Albums = null};

        context.Producers.Add(producerInDb);
        context.SaveChanges();

        var command = new DeleteProducerCommand(context);
        command.ProducerId = producerInDb.Id;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var producer = context.Producers.SingleOrDefault(b=> b.Id == command.ProducerId);
        producer.Should().BeNull();
    }
}