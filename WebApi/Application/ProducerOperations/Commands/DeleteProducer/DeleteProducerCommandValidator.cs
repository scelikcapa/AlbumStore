using FluentValidation;

namespace WebApi.Application.ProducerOperations.Commands.DeleteProducer;

public class DeleteProducerCommandValidator : AbstractValidator<DeleteProducerCommand>
{
    public DeleteProducerCommandValidator()
    {
        RuleFor(cmd => cmd.ProducerId).GreaterThan(0);
    }
}