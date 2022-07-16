using FluentAssertions;
using WebApi.Application.ProducerOperations.Commands.DeleteProducer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Commands.Delete;

public class DeleteProducerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenProducerIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteProducerCommand command = new DeleteProducerCommand(null);
        command.ProducerId = 0;

        // Act
        DeleteProducerCommandValidator validator = new DeleteProducerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenProducerIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteProducerCommand command = new DeleteProducerCommand(null);
        command.ProducerId = 1;
        
        // Act
        DeleteProducerCommandValidator validator = new DeleteProducerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
