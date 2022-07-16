using FluentAssertions;
using WebApi.Application.AlbumOperations.Queries.GetAlbumById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AlbumOperations.Queries.GetAlbumById;

public class GetAlbumByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenAlbumIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetAlbumByIdQuery command = new GetAlbumByIdQuery(null, null);
        command.AlbumId = 0;

        // Act
        GetAlbumByIdQueryValidator validator = new GetAlbumByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenAlbumIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetAlbumByIdQuery command = new GetAlbumByIdQuery(null, null);
        command.AlbumId = 1;
        
        // Act
        GetAlbumByIdQueryValidator validator = new GetAlbumByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
