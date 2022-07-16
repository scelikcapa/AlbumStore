using FluentValidation;

namespace WebApi.Application.ProducerOperations.Commands.UpdateProducer;

public class UpdateProducerCommandValidator : AbstractValidator<UpdateProducerCommand>
{
    public UpdateProducerCommandValidator()
    {
        RuleFor(cmd=>cmd.ProducerId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).MinimumLength(3).When(cmd=> cmd.Model.Name is not null);
        RuleFor(cmd=>cmd.Model.Surname).MinimumLength(2).When(cmd=> cmd.Model.Surname is not null);
    }
}