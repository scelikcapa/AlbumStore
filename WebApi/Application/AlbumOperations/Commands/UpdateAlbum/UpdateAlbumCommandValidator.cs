using FluentValidation;

namespace WebApi.Application.AlbumOperations.Commands.UpdateAlbum;

public class UpdateAlbumCommandValidator : AbstractValidator<UpdateAlbumCommand>
{
    public UpdateAlbumCommandValidator()
    {
        RuleFor(cmd=>cmd.AlbumId).GreaterThan(0);
        RuleFor(cmd=>cmd.Model.Title).MinimumLength(3).When(cmd=>cmd.Model.Title is not null);
        RuleFor(cmd=>cmd.Model.Year).LessThanOrEqualTo(DateTime.Now.Year).When(cmd=>cmd.Model.Year is not null);
        RuleFor(cmd=>cmd.Model.Price).GreaterThan(0).When(cmd=>cmd.Model.Price is not null);
        RuleFor(cmd=>cmd.Model.GenreId).GreaterThan(0).When(cmd=>cmd.Model.GenreId is not null);
        RuleFor(cmd=>cmd.Model.ProducerId).GreaterThan(0).When(cmd=>cmd.Model.ProducerId is not null);
    }
}