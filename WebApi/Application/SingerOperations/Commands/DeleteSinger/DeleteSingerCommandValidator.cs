using FluentValidation;

namespace WebApi.Application.SingerOperations.Commands.DeleteSinger;

public class DeleteSingerCommandValidator : AbstractValidator<DeleteSingerCommand>
{
    public DeleteSingerCommandValidator()
    {
        RuleFor(cmd => cmd.SingerId).GreaterThan(0);
    }
}