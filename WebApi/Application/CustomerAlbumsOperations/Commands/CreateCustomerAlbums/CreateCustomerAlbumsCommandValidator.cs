using FluentValidation;

namespace WebApi.Application.CustomerAlbumsOperations.Commands.CreateCustomerAlbums;

public class CreateCustomerAlbumsCommandValidator : AbstractValidator<CreateCustomerAlbumsCommand>
{
    public CreateCustomerAlbumsCommandValidator()
    {
        RuleFor(q=>q.CustomerId).GreaterThan(0);
        RuleFor(q=>q.Model.AlbumId).NotEmpty().GreaterThan(0);
    }
}