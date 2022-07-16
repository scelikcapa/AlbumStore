using FluentValidation;

namespace WebApi.Application.ProducerOperations.Commands.CreateProducer;

public class CreateProducerCommandValidator : AbstractValidator<CreateProducerCommand>
{
    public CreateProducerCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}