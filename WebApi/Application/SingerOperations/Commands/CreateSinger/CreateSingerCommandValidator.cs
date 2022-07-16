using FluentValidation;

namespace WebApi.Application.SingerOperations.Commands.CreateSinger;

public class CreateSingerCommandValidator : AbstractValidator<CreateSingerCommand>
{
    public CreateSingerCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Name).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Surname).NotEmpty().MinimumLength(2);
    }
}