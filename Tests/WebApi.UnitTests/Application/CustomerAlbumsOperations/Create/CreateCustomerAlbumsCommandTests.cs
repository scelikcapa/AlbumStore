using AutoMapper;
using FluentAssertions;
using WebApi.Application.CustomerAlbumsOperations.Commands.CreateCustomerAlbums;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerAlbumsOperations.Commands.Create;

public class CreateCustomerAlbumsCommandTests : IClassFixture<CommonTestFixture>
{   
    private readonly AlbumStoreDbContext context;
    private readonly IMapper mapper;

    public CreateCustomerAlbumsCommandTests(CommonTestFixture testFixture)
    {
        mapper = testFixture.Mapper;
        context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenCustomerIdDoesNotExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new CreateCustomerAlbumsCommand(context,mapper);
        command.CustomerId = -1;;

        // Act - Assert
        FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("CustomerId: " + command.CustomerId + " does not exist.");
    }

    [Fact]
    public void WhenGivenAlbumIdDoesNotExistsInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new CreateCustomerAlbumsCommand(context,mapper);
        command.CustomerId = 1;
        command.Model = new CreateCustomerAlbumsModel{AlbumId = -1};

        // Act - Assert
       FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("AlbumId:" + command.Model.AlbumId + " is not found.");
    }
}