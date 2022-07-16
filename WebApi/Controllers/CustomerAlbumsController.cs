using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerAlbumsOperations.Commands.CreateCustomerAlbums;
using WebApi.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("Customers")]
public class CustomerAlbumsController : ControllerBase
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public CustomerAlbumsController(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet("{id}/Albums")]
    public IActionResult GetCustomerAlbumsById(int id)
    {
        var query = new GetCustomerAlbumsByIdQuery(context,mapper);
        query.CustomerId = id;

        var validator = new GetCustomerAlbumsByIdQueryValidator();
        validator.ValidateAndThrow(query);

        var customerAlbums = query.Handle();

        return Ok(customerAlbums);
    }

    [HttpPost]
    [Route("{id}/Albums")]
    public IActionResult CreateCustomerAlbums(int id, [FromBody] CreateCustomerAlbumsModel purchasedAlbum)
    {
        var commmand = new CreateCustomerAlbumsCommand(context,mapper);
        commmand.CustomerId = id;
        commmand.Model = purchasedAlbum;

        var validator = new CreateCustomerAlbumsCommandValidator();
        validator.ValidateAndThrow(commmand);

        commmand.Handle();

        return Ok();
    }
}