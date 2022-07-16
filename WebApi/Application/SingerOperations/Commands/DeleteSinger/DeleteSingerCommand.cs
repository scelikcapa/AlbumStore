using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.SingerOperations.Commands.DeleteSinger;

public class DeleteSingerCommand 
{
    private readonly IAlbumStoreDbContext context;
    public int SingerId { get; set; }
    

    public DeleteSingerCommand(IAlbumStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var singerInDb = context.Singers.Include(a=> a.Albums).SingleOrDefault(m=>m.Id == SingerId);

        if(singerInDb is null)
            throw new InvalidOperationException("SingerId: "+SingerId+" does not exists.");
        
        if(singerInDb.Albums.Any())
            throw new InvalidOperationException("SingerId: " + SingerId + " has " +singerInDb.Albums.Count()+ " albums. Please delete them first.");
            
        context.Singers.Remove(singerInDb);
        
        context.SaveChanges();
    }
}
