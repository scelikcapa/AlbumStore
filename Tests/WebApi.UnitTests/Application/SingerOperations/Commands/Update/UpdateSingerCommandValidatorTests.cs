using FluentAssertions;
using WebApi.Application.SingerOperations.Commands.UpdateSinger;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Commands.Update;

public class UpdateSingerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, null, null)]
    [InlineData(1, "", null)]
    [InlineData(1, "Na", null)]
    [InlineData(1, null, "")]
    [InlineData(1, null, "S")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int singerId, string name, string surname)    
    {
        // Arrange
        UpdateSingerCommand command = new UpdateSingerCommand(null, null);
        command.SingerId = singerId;
        command.Model = new UpdateSingerModel{
            Name = name,
            Surname = surname};

        // Act
        UpdateSingerCommandValidator validator = new UpdateSingerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(1);        
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        UpdateSingerCommand command = new UpdateSingerCommand(null, null);
        command.SingerId = 1;
        command.Model = new UpdateSingerModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};

        // Act
        UpdateSingerCommandValidator validator = new UpdateSingerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
