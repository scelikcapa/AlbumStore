using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ProducerOperations.Queries.GetProducerById;

public class GetProducerByIdQuery 
{
    public int ProducerId { get; set; }
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;

    public GetProducerByIdQuery(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public GetProducerByIdViewModel Handle()
    {
        var producer = context.Producers.SingleOrDefault(m => m.Id == ProducerId);

        if(producer is null)
            throw new InvalidOperationException("ProducerId: "+ProducerId+" does not exist.");

        var producerViewModel = mapper.Map<GetProducerByIdViewModel>(producer);

        return producerViewModel;
    }
}

public class GetProducerByIdViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Album>? Albums { get; set; } 
}