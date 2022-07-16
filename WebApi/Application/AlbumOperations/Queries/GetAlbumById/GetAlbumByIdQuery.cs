using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AlbumOperations.Queries.GetAlbumById;

public class GetAlbumByIdQuery 
{
    public int AlbumId { get; set; }
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetAlbumByIdQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetAlbumByIdViewModel Handle()
    {
        var album = context.Albums.Include(m=> m.CustomerAlbums).SingleOrDefault(m => m.Id == AlbumId && m.IsActive == true);

        if(album is null)
            throw new InvalidOperationException("AlbumId: "+AlbumId+" does not exist.");

        var albumViewModel = mapper.Map<GetAlbumByIdViewModel>(album);

        return albumViewModel;
    }
}

public class GetAlbumByIdViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }

    public int GenreId { get; set; }
    public int ProducerId { get; set; }

    // public ICollection<Singer> Singers { get; set; }
    public List<CustomerAlbum> CustomerAlbums { get; set; }
}