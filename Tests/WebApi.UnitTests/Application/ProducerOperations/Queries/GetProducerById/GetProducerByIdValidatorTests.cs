using FluentAssertions;
using WebApi.Application.ProducerOperations.Queries.GetProducerById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Queries.GetProducerById;

public class GetProducerByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenProducerIdIsNotGreaterThenZero_Validator_ShouldBeReturnError()
    {
        // Arrange
        GetProducerByIdQuery command = new GetProducerByIdQuery(null, null);
        command.ProducerId = 0;

        // Act
        GetProducerByIdQueryValidator validator = new GetProducerByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenProducerIdIsGreaterThenZero__Validator_ShouldNotReturnError()
    {
        // Arrange
        GetProducerByIdQuery command = new GetProducerByIdQuery(null, null);
        command.ProducerId = 1;
        
        // Act
        GetProducerByIdQueryValidator validator = new GetProducerByIdQueryValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
