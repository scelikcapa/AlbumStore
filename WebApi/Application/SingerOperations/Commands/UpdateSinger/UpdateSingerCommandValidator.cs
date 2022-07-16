using FluentValidation;

namespace WebApi.Application.SingerOperations.Commands.UpdateSinger;

public class UpdateSingerCommandValidator : AbstractValidator<UpdateSingerCommand>
{
    public UpdateSingerCommandValidator()
    {
        RuleFor(cmd=>cmd.SingerId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Name).MinimumLength(3).When(cmd=> cmd.Model.Name is not null);
        RuleFor(cmd=>cmd.Model.Surname).MinimumLength(2).When(cmd=> cmd.Model.Surname is not null);
    }
}