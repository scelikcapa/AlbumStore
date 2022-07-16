using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.CustomerAlbumsOperations.Queries.GetCustomerAlbumsById;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerById;

public class GetCustomerByIdQuery 
{
    public int CustomerId { get; set; }
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetCustomerByIdQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetCustomerByIdViewModel Handle()
    {
        var customer = context.Customers.Include(c=> c.CustomerAlbums).SingleOrDefault(c => c.Id == CustomerId && c.IsActive == true);

        if(customer is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exist.");

        var customerViewModel = mapper.Map<GetCustomerByIdViewModel>(customer);

        return customerViewModel;
    }
}

public class GetCustomerByIdViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<GetCustomerAlbumsByIdViewModel> CustomerAlbums { get; set; }
}