using FluentAssertions;
using WebApi.Application.ProducerOperations.Commands.UpdateProducer;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.ProducerOperations.Commands.Update;

public class UpdateProducerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, null, null)]
    [InlineData(1, "", null)]
    [InlineData(1, "Na", null)]
    [InlineData(1, null, "")]
    [InlineData(1, null, "S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int producerId, string name, string surname)    
    {
        // Arrange
        UpdateProducerCommand command = new UpdateProducerCommand(null, null);
        command.ProducerId = producerId;
        command.Model = new UpdateProducerModel{
            Name = name,
            Surname = surname};

        // Act
        UpdateProducerCommandValidator validator = new UpdateProducerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateProducerCommand command = new UpdateProducerCommand(null, null);
        command.ProducerId = 1;
        command.Model = new UpdateProducerModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};

        // Act
        UpdateProducerCommandValidator validator = new UpdateProducerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
