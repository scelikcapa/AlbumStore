using FluentAssertions;
using WebApi.Application.SingerOperations.Queries.GetSingerById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Queries.GetSingerById;

public class GetSingerByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenSingerIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetSingerByIdQuery command = new GetSingerByIdQuery(null, null);
        command.SingerId = 0;

        // Act
        GetSingerByIdQueryValidator validator = new GetSingerByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenSingerIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetSingerByIdQuery command = new GetSingerByIdQuery(null, null);
        command.SingerId = 1;
        
        // Act
        GetSingerByIdQueryValidator validator = new GetSingerByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
