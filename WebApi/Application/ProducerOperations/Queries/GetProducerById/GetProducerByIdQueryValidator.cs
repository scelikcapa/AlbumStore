using FluentValidation;

namespace WebApi.Application.ProducerOperations.Queries.GetProducerById;

public class GetProducerByIdQueryValidator : AbstractValidator<GetProducerByIdQuery>
{
    public GetProducerByIdQueryValidator()
    {
        RuleFor(property=>property.ProducerId).GreaterThan(0);
    }
}