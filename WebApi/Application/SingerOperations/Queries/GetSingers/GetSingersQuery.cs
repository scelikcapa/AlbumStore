using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.SingerOperations.Queries.GetSingers;

public class GetSingersQuery 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetSingersQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetSingersViewModel> Handle()
    {
        var singers = context.Singers.OrderBy(m=>m.Id).ToList();

        var singersViewModel = mapper.Map<List<GetSingersViewModel>>(singers);

        return singersViewModel;
    }
}

public class GetSingersViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Album>? Albums { get; set; }
}