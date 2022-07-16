using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ProducerOperations.Queries.GetProducers;

public class GetProducersQuery 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetProducersQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GetProducersViewModel> Handle()
    {
        var producers = context.Producers.OrderBy(m=>m.Id).ToList();

        var producersViewModel = mapper.Map<List<GetProducersViewModel>>(producers);

        return producersViewModel;
    }
}

public class GetProducersViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Album>? Albums { get; set; }
}