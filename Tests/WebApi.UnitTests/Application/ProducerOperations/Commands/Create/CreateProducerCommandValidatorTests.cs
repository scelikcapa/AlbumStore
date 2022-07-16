using FluentAssertions;
using WebApi.Application.ProducerOperations.Commands.CreateProducer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Commands.Create;

public class CreateProducerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null, "Surname")]
    [InlineData("", "Surname")]
    [InlineData("Na", "Surname")]
    [InlineData("Nam", null)]
    [InlineData("Nam", "")]
    [InlineData("Nam", "S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
    {
        // Arrange
        CreateProducerCommand command = new CreateProducerCommand(null, null);
        command.Model = new CreateProducerModel{
            Name = name, 
            Surname = surname};

        // Act
        CreateProducerCommandValidator validator = new CreateProducerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeInRange(1,2);      
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateProducerCommand command = new CreateProducerCommand(null, null);
        command.Model = new CreateProducerModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};
            
        // Act
        CreateProducerCommandValidator validator = new CreateProducerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
