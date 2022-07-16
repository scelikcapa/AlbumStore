using AutoMapper;
using FluentAssertions;
using WebApi.Application.SingerOperations.Commands.CreateSinger;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Commands.Create;

public class CreateSingerCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public CreateSingerCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var singerInDb = new Singer{ 
                        Name = "WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn", 
                        Surname = "WhenGivenSingerNameAlreadyExistsInDb_InvalidOperationException_ShouldBeReturn" };

        context.Singers.Add(singerInDb);
        context.SaveChanges();

        var command = new CreateSingerCommand(context,mapper);
        command.Model = new CreateSingerModel{
                            Name = singerInDb.Name,
                            Surname = singerInDb.Surname};

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("SingerNameSurname: " + command.Model.Name+" "+command.Model.Surname + " already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Singer_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateSingerCommand(context,mapper);
        command.Model = new CreateSingerModel{
                            Name = "WhenValidInputsAreGiven_Singer_ShouldBeCreated", 
                            Surname = "WhenValidInputsAreGiven_Singer_ShouldBeCreated"};

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var singerCreated = context.Singers.SingleOrDefault(b=> b.Name == command.Model.Name && b.Surname == command.Model.Surname);
        singerCreated.Should().NotBeNull();
        singerCreated.Name.Should().Be(command.Model.Name);
        singerCreated.Surname.Should().Be(command.Model.Surname);
    }
}