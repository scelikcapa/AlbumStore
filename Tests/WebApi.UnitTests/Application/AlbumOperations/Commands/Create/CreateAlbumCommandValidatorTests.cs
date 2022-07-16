using FluentAssertions;
using WebApi.Application.AlbumOperations.Commands.CreateAlbum;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Commands.Create;

public class CreateAlbumCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null, 10, 1, 1)]
    [InlineData("", 10, 1, 1)]
    [InlineData("Ti", 10, 1, 1)]
    [InlineData("Title", 0, 1, 1)]
    [InlineData("Title", 1, 0, 1)]
    [InlineData("Title", 1, 1, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, double price, int genreId, int producerId)
    {
        // Arrange
        CreateAlbumCommand command = new CreateAlbumCommand(null, null);
        command.Model = new CreateAlbumModel{
            Title = title, 
            Year = 1991, 
            Price = price, 
            GenreId = genreId, 
            ProducerId = producerId};

        // Act
        CreateAlbumCommandValidator validator = new CreateAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeInRange(1,2);      
    }

    [Fact]
    public void WhenDateTimeGreaterThenNowIsGiven_Validator_ShouldReturnError()
    {
        // Arrange
        CreateAlbumCommand command = new CreateAlbumCommand(null, null);
        command.Model = new CreateAlbumModel{
            Title = "CreatingAlbum",
            Year = DateTime.Now.Year + 1,
            Price = 10,
            GenreId = 1,
            ProducerId = 1
        };
        // Act
        CreateAlbumCommandValidator validator = new CreateAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateAlbumCommand command = new CreateAlbumCommand(null, null);
        command.Model = new CreateAlbumModel{
            Title = "CreatingAlbum",
            Year = DateTime.Now.Year,
            Price = 10,
            GenreId = 1,
            ProducerId = 1
        };
        // Act
        CreateAlbumCommandValidator validator = new CreateAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
