using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AlbumOperations.Queries.GetAlbums;

public class GetAlbumsQuery 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetAlbumsQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetAlbumsViewModel> Handle()
    {
        var albums = context.Albums.Include(m=> m.CustomerAlbums).Where(m => m.IsActive == true).OrderBy(m=>m.Id).ToList();

        var albumsViewModel = mapper.Map<List<GetAlbumsViewModel>>(albums);

        return albumsViewModel;
    }
}

public class GetAlbumsViewModel
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