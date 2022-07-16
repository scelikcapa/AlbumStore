using FluentValidation;

namespace WebApi.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;

public class GetCustomerAlbumsByIdQueryValidator : AbstractValidator<GetCustomerAlbumsByIdQuery>
{
    public GetCustomerAlbumsByIdQueryValidator()
    {
        RuleFor(q=>q.CustomerId).GreaterThan(0);
    }
}