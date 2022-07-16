using AutoMapper;
using FluentAssertions;
using WebApi.Application.ProducerOperations.Commands.CreateProducer;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Commands.Create;

public class CreateProducerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public CreateProducerCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var producerInDb = new Producer{ 
                        Name = "WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenProducerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn" };

        context.Producers.Add(producerInDb);
        context.SaveChanges();

        var command = new CreateProducerCommand(context,mapper);
        command.Model = new CreateProducerModel{
                            Name = producerInDb.Name,
                            Surname = producerInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ProducerNameSurname: " + command.Model.Name+" "+command.Model.Surname + " already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Producer_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateProducerCommand(context,mapper);
        command.Model = new CreateProducerModel{
                            Name = "WhenValidInputsAreGiven_Producer_ShouldBeCreated", 
                            Surname = "WhenValidInputsAreGiven_Producer_ShouldBeCreated"};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var producerCreated = context.Producers.SingleOrDefault(b=> b.Name == command.Model.Name && b.Surname == command.Model.Surname);
        producerCreated.Should().NotBeNull();
        producerCreated.Name.Should().Be(command.Model.Name);
        producerCreated.Surname.Should().Be(command.Model.Surname);
    }
}