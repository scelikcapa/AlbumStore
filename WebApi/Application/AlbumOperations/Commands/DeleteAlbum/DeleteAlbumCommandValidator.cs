using FluentValidation;

namespace WebApi.Application.AlbumOperations.Commands.DeleteAlbum;

public class DeleteAlbumCommandValidator : AbstractValidator<DeleteAlbumCommand>
{
    public DeleteAlbumCommandValidator()
    {
        RuleFor(cmd => cmd.AlbumId).GreaterThan(0);
    }
}