using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AlbumOperations.Commands.UpdateAlbum;

public class UpdateAlbumCommand 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    
    public int AlbumId { get; set; }
    public UpdateAlbumModel Model { get; set; }
    

    public UpdateAlbumCommand(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var albumInDb = context.Albums.SingleOrDefault(m=>m.Id == AlbumId);

        if(albumInDb is null)
            throw new InvalidOperationException("AlbumId: "+AlbumId+" does not exist.");
        
        bool isSameTitleExists = context.Albums.Where(m=>
                                                    m.Title.ToLower() == (Model.Title != null ? Model.Title.ToLower() : albumInDb.Title.ToLower()) && 
                                                    m.Id != AlbumId)
                                                .Any();
        
        if(isSameTitleExists)
            throw new InvalidOperationException("AlbumTitle: "+ Model.Title +" already exists, choose another name.");

        mapper.Map<UpdateAlbumModel, Album>(Model, albumInDb);

        context.SaveChanges();
    }
}

public class UpdateAlbumModel 
{
    public string? Title { get; set; }
    public int? Year { get; set; }
    public double? Price { get; set; }

    public int? GenreId { get; set; }    
    public int? ProducerId { get; set; }
    
    public bool? IsActive { get; set; }
    
}