using AutoMapper;
using FluentAssertions;
using WebApi.Application.ProducerOperations.Commands.UpdateProducer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Commands.Update;

public class UpdateProducerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateProducerCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenProducerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateProducerCommand(context, mapper);
        command.ProducerId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ProducerId: " + command.ProducerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var producerInDb = new Producer{ 
                        Name = "WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1", 
                        Surname = "WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1"};

        var producerUpdating = new Producer{ 
                        Name = "WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2", 
                        Surname = "WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2"};

        context.Producers.Add(producerInDb);
        context.Producers.Add(producerUpdating);
        context.SaveChanges();

        var command = new UpdateProducerCommand(context,mapper);
        command.ProducerId = producerUpdating.Id;
        command.Model = new UpdateProducerModel{
                            Name = producerInDb.Name,
                            Surname = producerInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ProducerNameSurname: "+ command.Model.Name+" "+ command.Model.Surname+" already exists, choose another name.");
    }

    [Fact]
    public void WhenGivenProducerIdExistsInDb_Producer_ShouldBeUpdated()
    {
        // Arrange
        var producerInDb = new Producer{ 
                        Name = "WhenGivenProducerIdExistsInDb_Producer_ShouldBeUpdated", 
                        Surname = "WhenGivenProducerIdExistsInDb_Producer_ShouldBeUpdated"};

        var producerCompared = new Producer{ 
                            Name = producerInDb.Name,
                            Surname = producerInDb.Surname};

        context.Producers.Add(producerInDb);
        context.SaveChanges();

        var command = new UpdateProducerCommand(context,mapper);
        command.ProducerId = producerInDb.Id;
        command.Model = new UpdateProducerModel{
                            Name = "WhenGivenProducerIdExistsInDb_Producer_ShouldBeUpdated2", 
                            Surname = "WhenGivenProducerIdExistsInDb_Producer_ShouldBeUpdated2"};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var producerUpdated = context.Producers.SingleOrDefault(b=> b.Id == producerInDb.Id);
        producerUpdated.Should().NotBeNull();
        producerUpdated.Name.Should().NotBe(producerCompared.Name);
        producerUpdated.Surname.Should().NotBe(producerCompared.Surname);

    }
}