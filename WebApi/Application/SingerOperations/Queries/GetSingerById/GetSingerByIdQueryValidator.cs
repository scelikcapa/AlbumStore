using FluentValidation;

namespace WebApi.Application.SingerOperations.Queries.GetSingerById;

public class GetSingerByIdQueryValidator : AbstractValidator<GetSingerByIdQuery>
{
    public GetSingerByIdQueryValidator()
    {
        RuleFor(property=>property.SingerId).GreaterThan(0);
    }
}