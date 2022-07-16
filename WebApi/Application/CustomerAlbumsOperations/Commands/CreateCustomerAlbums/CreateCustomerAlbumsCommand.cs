using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerAlbumsOperations.Commands.CreateCustomerAlbums;

public class CreateCustomerAlbumsCommand 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    public int CustomerId { get; set; }

    public CreateCustomerAlbumsModel Model { get; set; }


    public CreateCustomerAlbumsCommand(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var customer = context.Customers.Include(c=> c.CustomerAlbums).SingleOrDefault(c=> c.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");

        var albumAdding = context.Albums.SingleOrDefault(m=> m.Id == Model.AlbumId && m.IsActive == true);

        if(albumAdding is null)
            throw new InvalidOperationException("AlbumId:" + Model.AlbumId + " is not found."); 

        customer.CustomerAlbums.Add(
            new CustomerAlbum{
                Price = albumAdding.Price,
                OrderDate = DateTime.Now,
                Album = albumAdding
        });
        
        context.SaveChanges();
    }
}

public class CreateCustomerAlbumsModel
{  
    public int AlbumId { get; set; }         
}