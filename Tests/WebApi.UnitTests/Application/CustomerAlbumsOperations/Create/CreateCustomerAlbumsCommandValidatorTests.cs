using FluentAssertions;
using WebApi.Application.CustomerAlbumsOperations.Commands.CreateCustomerAlbums;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerAlbumsOperations.Commands.Create;

public class CreateCustomerAlbumsCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int customerId, int albumId)
    {
        // Arrange
        var command = new CreateCustomerAlbumsCommand(null, null);
        command.CustomerId = customerId;
        command.Model = new CreateCustomerAlbumsModel{AlbumId = albumId};

        // Act
        CreateCustomerAlbumsCommandValidator validator = new CreateCustomerAlbumsCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);      
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        var command = new CreateCustomerAlbumsCommand(null, null);
        command.CustomerId = 1;
        command.Model = new CreateCustomerAlbumsModel{AlbumId = 1};
            
        // Act
        CreateCustomerAlbumsCommandValidator validator = new CreateCustomerAlbumsCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
