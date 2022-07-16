using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AlbumOperations.Commands.CreateAlbum;

public class CreateAlbumCommand 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    public CreateAlbumModel Model { get; set; }
    

    public CreateAlbumCommand(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var albumInDb = context.Albums.SingleOrDefault(m=>m.Title.ToLower() == Model.Title.ToLower());

        if(albumInDb is not null)
            throw new InvalidOperationException("AlbumTitle: " + Model.Title + " already exists, choose another name.");

        var newAlbum = mapper.Map<Album>(Model);

        context.Albums.Add(newAlbum);
        context.SaveChanges();
    }
}

public class CreateAlbumModel 
{
    public string Title { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }

    public int GenreId { get; set; }    
    public int ProducerId { get; set; }
    
}