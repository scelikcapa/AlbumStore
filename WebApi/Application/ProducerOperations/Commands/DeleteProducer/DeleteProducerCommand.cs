using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ProducerOperations.Commands.DeleteProducer;

public class DeleteProducerCommand 
{
    private readonly IAlbumStoreDbContext context;
    public int ProducerId { get; set; }
    

    public DeleteProducerCommand(IAlbumStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var producerInDb = context.Producers.Include(d=> d.Albums).SingleOrDefault(m=>m.Id == ProducerId);

        if(producerInDb is null)
            throw new InvalidOperationException("ProducerId: "+ProducerId+" does not exists.");
        
        if(producerInDb.Albums.Any())
            throw new InvalidOperationException("ProducerId: " + ProducerId + " has " +producerInDb.Albums.Count()+ " albums. Please delete them first.");
            
        context.Producers.Remove(producerInDb);
        
        context.SaveChanges();
    }
}
