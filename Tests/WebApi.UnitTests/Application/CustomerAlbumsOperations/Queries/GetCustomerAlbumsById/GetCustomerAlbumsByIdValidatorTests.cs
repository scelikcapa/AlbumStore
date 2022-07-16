using FluentAssertions;
using WebApi.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;

public class GetCustomerAlbumsByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenCustomerIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetCustomerAlbumsByIdQuery command = new GetCustomerAlbumsByIdQuery(null, null);
        command.CustomerId = 0;

        // Act
        GetCustomerAlbumsByIdQueryValidator validator = new GetCustomerAlbumsByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenCustomerIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetCustomerAlbumsByIdQuery command = new GetCustomerAlbumsByIdQuery(null, null);
        command.CustomerId = 1;
        
        // Act
        GetCustomerAlbumsByIdQueryValidator validator = new GetCustomerAlbumsByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
