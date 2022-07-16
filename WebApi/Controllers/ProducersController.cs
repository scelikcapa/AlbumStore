using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ProducerOperations.Commands.CreateProducer;
using WebApi.Application.ProducerOperations.Commands.DeleteProducer;
using WebApi.Application.ProducerOperations.Commands.UpdateProducer;
using WebApi.Application.ProducerOperations.Queries.GetProducerById;
using WebApi.Application.ProducerOperations.Queries.GetProducers;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProducersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IAlbumStoreDbContext context;

    public ProducersController(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

     [HttpGet]
    public IActionResult GetProducers()
    {
        var query = new GetProducersQuery(context,mapper);
        var albums = query.Handle();

        return Ok(albums);
    }

    [HttpGet("{id}")]
    public IActionResult GetProducerById(int id)
    {
        var query = new GetProducerByIdQuery(context,mapper);
        query.ProducerId = id;

        var validator = new GetProducerByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var album = query.Handle();

        return Ok(album);
    }

    [HttpPost]
    public IActionResult CreateProducer([FromBody] CreateProducerModel newProducer)
    {
        var command = new CreateProducerCommand(context,mapper);
        command.Model=newProducer;

        var validator = new CreateProducerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProducer(int id, [FromBody] UpdateProducerModel updatedProducer)
    {
        var command = new UpdateProducerCommand(context,mapper);
        command.ProducerId = id;
        command.Model=updatedProducer;

        var validator = new UpdateProducerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProducer(int id)
    {
        var command = new DeleteProducerCommand(context);
        command.ProducerId = id;

        var validator = new DeleteProducerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}