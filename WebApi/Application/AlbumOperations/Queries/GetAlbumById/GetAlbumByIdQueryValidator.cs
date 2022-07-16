using FluentValidation;

namespace WebApi.Application.AlbumOperations.Queries.GetAlbumById;

public class GetAlbumByIdQueryValidator : AbstractValidator<GetAlbumByIdQuery>
{
    public GetAlbumByIdQueryValidator()
    {
        RuleFor(property=>property.AlbumId).GreaterThan(0);
    }
}