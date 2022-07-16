using FluentAssertions;
using WebApi.Application.AlbumOperations.Commands.UpdateAlbum;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Commands.Update;

public class UpdateAlbumCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, null, null, null, null)]
    [InlineData(1, "AI", null, null, null)]
    [InlineData(1, null, 0, null, null)]
    [InlineData(1, null, null, 0, null)]
    [InlineData(1, null, null, null, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int albumId, string title, double? price, int? genreId, int? producerId)    
    {
        // Arrange
        UpdateAlbumCommand command = new UpdateAlbumCommand(null, null);
        command.AlbumId = albumId;
        command.Model = new UpdateAlbumModel{
            Title = title,
            Price = price,
            GenreId = genreId,
            ProducerId = producerId
        };

        // Act
        UpdateAlbumCommandValidator validator = new UpdateAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenYearGreaterThenThisYearIsGiven_Validator_ShouldReturnError()
    {
        // Arrange
        UpdateAlbumCommand command = new UpdateAlbumCommand(null, null);
        command.AlbumId = 1;
        command.Model = new UpdateAlbumModel{
            Year = DateTime.Now.Year + 1
        };
        // Act
        UpdateAlbumCommandValidator validator = new UpdateAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);   
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateAlbumCommand command = new UpdateAlbumCommand(null, null);
        command.AlbumId = 1;
        command.Model = new UpdateAlbumModel{
            Title = "title",
            Year = 2022,
            Price = 22,
            GenreId = 2,
            ProducerId = 2
        };
        // Act
        UpdateAlbumCommandValidator validator = new UpdateAlbumCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
