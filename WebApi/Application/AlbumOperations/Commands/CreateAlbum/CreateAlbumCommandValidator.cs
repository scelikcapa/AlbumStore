using FluentValidation;

namespace WebApi.Application.AlbumOperations.Commands.CreateAlbum;

public class CreateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
{
    public CreateAlbumCommandValidator()
    {
        RuleFor(cmd=>cmd.Model.Title).NotEmpty().MinimumLength(3);
        RuleFor(cmd=>cmd.Model.Year).NotEmpty().LessThanOrEqualTo(DateTime.Now.Year);
        RuleFor(cmd=>cmd.Model.Price).NotEmpty().GreaterThan(0);
        RuleFor(cmd=>cmd.Model.GenreId).NotEmpty().GreaterThan(0);
        RuleFor(cmd=>cmd.Model.ProducerId).NotEmpty().GreaterThan(0);
    }
}