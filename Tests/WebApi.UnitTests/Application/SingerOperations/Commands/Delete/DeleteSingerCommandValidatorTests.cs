using FluentAssertions;
using WebApi.Application.SingerOperations.Commands.DeleteSinger;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.SingerOperations.Commands.Delete;

public class DeleteSingerCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenGivenSingerIdIsNotGreaterThenZero_Validator_ShouldReturnError()
    {
        // Arrange
        DeleteSingerCommand command = new DeleteSingerCommand(null);
        command.SingerId = 0;

        // Act
        DeleteSingerCommandValidator validator = new DeleteSingerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().BeGreaterThan(0);        
    }

    [Fact]
    public void WhenGivenSingerIdIsGreaterThenZero_Validator_ShouldNotReturnError()
    {
        // Arrange
        DeleteSingerCommand command = new DeleteSingerCommand(null);
        command.SingerId = 1;
        
        // Act
        DeleteSingerCommandValidator validator = new DeleteSingerCommandValidator();
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.Errors.Count.Should().Be(0);   
    }

}
