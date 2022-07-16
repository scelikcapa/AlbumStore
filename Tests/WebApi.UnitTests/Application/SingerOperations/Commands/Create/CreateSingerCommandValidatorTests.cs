using FluentAssertions;
using WebApi.Application.SingerOperations.Commands.CreateSinger;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Commands.Create;

public class CreateSingerCommandValidatorTests : IClassFixture<CommonTestFixture>
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
        CreateSingerCommand command = new CreateSingerCommand(null, null);
        command.Model = new CreateSingerModel{
            Name = name, 
            Surname = surname};

        // Act
        CreateSingerCommandValidator validator = new CreateSingerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeInRange(1,2);      
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        CreateSingerCommand command = new CreateSingerCommand(null, null);
        command.Model = new CreateSingerModel{
            Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
            Surname = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError"};
            
        // Act
        CreateSingerCommandValidator validator = new CreateSingerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
