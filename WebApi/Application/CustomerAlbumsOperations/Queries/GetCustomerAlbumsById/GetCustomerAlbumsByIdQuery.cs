using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;

public class GetCustomerAlbumsByIdQuery 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    public int CustomerId { get; set; }

    public GetCustomerAlbumsByIdQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetCustomerAlbumsByIdViewModel> Handle()
    {
        var customer = context.Customers.Include(c => c.CustomerAlbums).SingleOrDefault(c => c.Id == CustomerId);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");

        if(!customer.CustomerAlbums.Any())
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not have any album.");

        var customerAlbums = mapper.Map<List<GetCustomerAlbumsByIdViewModel>>(customer.CustomerAlbums.OrderBy(cm=> cm.Id));

        return customerAlbums;
    }
}

public class GetCustomerAlbumsByIdViewModel
{
    public int Id { get; set; }     
    public double Price { get; set; }
    public string OrderDate { get; set; }

    public int AlbumId { get; set; }         
}