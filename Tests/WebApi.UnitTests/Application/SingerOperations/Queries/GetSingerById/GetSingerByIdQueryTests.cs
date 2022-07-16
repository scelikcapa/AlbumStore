using AutoMapper;
using FluentAssertions;
using WebApi.Application.SingerOperations.Queries.GetSingerById;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Queries.GetSingerById;

public class GetSingerByIdQueryTests : IClassFixture<CommonTestFixture> 
{
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetSingerByIdQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

     [Fact]
    public void WhenGivenSingerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var singer = new Singer{ 
            Name = "WhenGivenSingerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn", 
            Surname = "WhenGivenSingerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn"};

        context.Singers.Add(singer);
        context.SaveChanges();
        
        context.Singers.Remove(singer);
        context.SaveChanges();

        GetSingerByIdQuery command = new GetSingerByIdQuery(context, mapper);
        command.SingerId= singer.Id;

        // act - assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("SingerId: " + command.SingerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenSingerIdDoesExistInDb_Singer_ShouldBeReturned()
    {
        // Arrange
        var singer = new Singer{ 
            Name = "WhenGivenSingerIdDoesExistInDb_Singer_ShouldBeReturned", 
            Surname = "WhenGivenSingerIdDoesExistInDb_Singer_ShouldBeReturned"};
        context.Singers.Add(singer);
        context.SaveChanges();

        GetSingerByIdQuery command = new GetSingerByIdQuery(context, mapper);
        command.SingerId = singer.Id;

        // Act
        var singerReturned = FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        singerReturned.Should().NotBeNull();
        singerReturned.Id.Should().Be(command.SingerId);
        singerReturned.Name.Should().Be(singer.Name);
        singerReturned.Surname.Should().Be(singer.Surname);
    }
}