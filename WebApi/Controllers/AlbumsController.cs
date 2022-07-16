using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AlbumOperations.Commands.CreateAlbum;
using WebApi.Application.AlbumOperations.Commands.DeleteAlbum;
using WebApi.Application.AlbumOperations.Commands.UpdateAlbum;
using WebApi.Application.AlbumOperations.Queries.GetAlbumById;
using WebApi.Application.AlbumOperations.Queries.GetAlbums;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public AlbumsController(IAlbumStoreDbContext context,IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAlbums()
    {
        var query = new GetAlbumsQuery(context,mapper);
        var albums = query.Handle();

        return Ok(albums);
    }

    [HttpGet("{id}")]
    public IActionResult GetAlbumById(int id)
    {
        var query = new GetAlbumByIdQuery(context,mapper);
        query.AlbumId = id;

        var validator = new GetAlbumByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var album = query.Handle();

        return Ok(album);
    }

    [HttpPost]
    public IActionResult CreateAlbum([FromBody] CreateAlbumModel newAlbum)
    {
        var command = new CreateAlbumCommand(context,mapper);
        command.Model=newAlbum;

        var validator = new CreateAlbumCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAlbum(int id, [FromBody] UpdateAlbumModel updatedAlbum)
    {
        var command = new UpdateAlbumCommand(context,mapper);
        command.AlbumId = id;
        command.Model=updatedAlbum;

        var validator = new UpdateAlbumCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAlbum(int id)
    {
        var command = new DeleteAlbumCommand(context);
        command.AlbumId = id;

        var validator = new DeleteAlbumCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}