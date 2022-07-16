using AutoMapper;
using FluentAssertions;
using WebApi.Application.SingerOperations.Commands.UpdateSinger;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Commands.Update;

public class UpdateSingerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateSingerCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenSingerIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateSingerCommand(context, mapper);
        command.SingerId = -1;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("SingerId: " + command.SingerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var singerInDb = new Singer{ 
                        Name = "WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1", 
                        Surname = "WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn1"};

        var singerUpdating = new Singer{ 
                        Name = "WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2", 
                        Surname = "WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn2"};

        context.Singers.Add(singerInDb);
        context.Singers.Add(singerUpdating);
        context.SaveChanges();

        var command = new UpdateSingerCommand(context,mapper);
        command.SingerId = singerUpdating.Id;
        command.Model = new UpdateSingerModel{
                            Name = singerInDb.Name,
                            Surname = singerInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("SingerNameSurname: "+ command.Model.Name+" "+ command.Model.Surname+" already exists, choose another name.");
    }

    [Fact]
    public void WhenGivenSingerIdExistsInDb_Singer_ShouldBeUpdated()
    {
        // Arrange
        var singerInDb = new Singer{ 
                        Name = "WhenGivenSingerIdExistsInDb_Singer_ShouldBeUpdated", 
                        Surname = "WhenGivenSingerIdExistsInDb_Singer_ShouldBeUpdated"};

        var singerCompared = new Singer{ 
                            Name = singerInDb.Name,
                            Surname = singerInDb.Surname};

        context.Singers.Add(singerInDb);
        context.SaveChanges();

        var command = new UpdateSingerCommand(context,mapper);
        command.SingerId = singerInDb.Id;
        command.Model = new UpdateSingerModel{
                            Name = "WhenGivenSingerIdExistsInDb_Singer_ShouldBeUpdated2", 
                            Surname = "WhenGivenSingerIdExistsInDb_Singer_ShouldBeUpdated2"};
        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var singerUpdated = context.Singers.SingleOrDefault(b=> b.Id == singerInDb.Id);
        singerUpdated.Should().NotBeNull();
        singerUpdated.Name.Should().NotBe(singerCompared.Name);
        singerUpdated.Surname.Should().NotBe(singerCompared.Surname);

    }
}