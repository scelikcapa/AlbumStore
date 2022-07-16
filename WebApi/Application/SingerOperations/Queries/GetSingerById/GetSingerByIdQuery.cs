using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.SingerOperations.Queries.GetSingerById;

public class GetSingerByIdQuery 
{
    public int SingerId { get; set; }
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetSingerByIdQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetSingerByIdViewModel Handle()
    {
        var singer = context.Singers.Include(a=> a.Albums).SingleOrDefault(m => m.Id == SingerId);

        if(singer is null)
            throw new InvalidOperationException("SingerId: "+SingerId+" does not exist.");

        var singerViewModel = mapper.Map<GetSingerByIdViewModel>(singer);

        return singerViewModel;
    }
}

public class GetSingerByIdViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Album>? Albums { get; set; }
}