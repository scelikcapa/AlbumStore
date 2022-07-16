using AutoMapper;
using FluentAssertions;
using WebApi.Application.ProducerOperations.Queries.GetProducerById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Queries.GetProducerById;

public class GetProducerByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetProducerByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenProducerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var producer = new Producer{ 
            Name = "WhenGivenProducerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Surname = "WhenGivenProducerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn"};

        context.Producers.Add(producer);
        context.SaveChanges();
        
        context.Producers.Remove(producer);
        context.SaveChanges();

        GetProducerByIdQuery command = new GetProducerByIdQuery(context, mapper);
        command.ProducerId= producer.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("ProducerId: " + command.ProducerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenProducerIdDoesExistInDb_Producer_ShouldBeReturned()
    {
        // Arrange
        var producer = new Producer{ 
            Name = "WhenGivenProducerIdDoesExistInDb_Producer_ShouldBeReturned", 
            Surname = "WhenGivenProducerIdDoesExistInDb_Producer_ShouldBeReturned"};
        context.Producers.Add(producer);
        context.SaveChanges();

        GetProducerByIdQuery command = new GetProducerByIdQuery(context, mapper);
        command.ProducerId = producer.Id;

        // Act
        var producerReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        producerReturned.Should().NotBeNull();
        producerReturned.Id.Should().Be(command.ProducerId);
        producerReturned.Name.Should().Be(producer.Name);
        producerReturned.Surname.Should().Be(producer.Surname);
    }
}