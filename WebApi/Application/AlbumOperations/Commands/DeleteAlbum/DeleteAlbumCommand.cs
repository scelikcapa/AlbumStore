using WebApi.DbOperations;

namespace WebApi.Application.AlbumOperations.Commands.DeleteAlbum;

public class DeleteAlbumCommand 
{
    private readonly IAlbumStoreDbContext context;
    public int AlbumId { get; set; }
    

    public DeleteAlbumCommand(IAlbumStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var albumInDb = context.Albums.SingleOrDefault(m=>m.Id == AlbumId);

        if(albumInDb is null)
            throw new InvalidOperationException("Album Id: "+AlbumId+" does not exists.");
        
        albumInDb.IsActive = false;
        
        context.SaveChanges();
    }
}
