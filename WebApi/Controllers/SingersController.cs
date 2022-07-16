using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.SingerOperations.Commands.CreateSinger;
using WebApi.Application.SingerOperations.Commands.DeleteSinger;
using WebApi.Application.SingerOperations.Commands.UpdateSinger;
using WebApi.Application.SingerOperations.Queries.GetSingerById;
using WebApi.Application.SingerOperations.Queries.GetSingers;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class SingersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IAlbumStoreDbContext context;

    public SingersController(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

     [HttpGet]
    public IActionResult GetSingers()
    {
        var query = new GetSingersQuery(context,mapper);
        var albums = query.Handle();

        return Ok(albums);
    }

    [HttpGet("{id}")]
    public IActionResult GetSingerById(int id)
    {
        var query = new GetSingerByIdQuery(context,mapper);
        query.SingerId = id;

        var validator = new GetSingerByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var album = query.Handle();

        return Ok(album);
    }

    [HttpPost]
    public IActionResult CreateSinger([FromBody] CreateSingerModel newSinger)
    {
        var command = new CreateSingerCommand(context,mapper);
        command.Model=newSinger;

        var validator = new CreateSingerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSinger(int id, [FromBody] UpdateSingerModel updatedSinger)
    {
        var command = new UpdateSingerCommand(context,mapper);
        command.SingerId = id;
        command.Model=updatedSinger;

        var validator = new UpdateSingerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSinger(int id)
    {
        var command = new DeleteSingerCommand(context);
        command.SingerId = id;

        var validator = new DeleteSingerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}