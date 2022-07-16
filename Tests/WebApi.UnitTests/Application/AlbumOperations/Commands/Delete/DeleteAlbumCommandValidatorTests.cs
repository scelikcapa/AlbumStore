using FluentAssertions;
using WebApi.Application.AlbumOperations.Commands.DeleteAlbum;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Commands.Delete;

public class DeleteAlbumCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenAlbumIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteAlbumCommand command = new DeleteAlbumCommand(null);
        command.AlbumId = 0;

        // Act
        DeleteAlbumCommandValidator validator = new DeleteAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenAlbumIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteAlbumCommand command = new DeleteAlbumCommand(null);
        command.AlbumId = 1;
        
        // Act
        DeleteAlbumCommandValidator validator = new DeleteAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
