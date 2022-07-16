using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommand 
{
    private readonly IAlbumStoreDbContext context;
    public int CustomerId { get; set; }
    

    public DeleteCustomerCommand(IAlbumStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var customerInDb = context.Customers.SingleOrDefault(m=>m.Id == CustomerId);

        if(customerInDb is null)
            throw new InvalidOperationException("CustomerId: "+CustomerId+" does not exists.");
        
        customerInDb.IsActive = false;
        
        context.SaveChanges();
    }
}
