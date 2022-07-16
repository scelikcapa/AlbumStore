using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ProducerOperations.Commands.CreateProducer;

public class CreateProducerCommand 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    public CreateProducerModel Model { get; set; }
    

    public CreateProducerCommand(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var producerInDb = context.Producers.SingleOrDefault(m=>m.Name.ToLower() == Model.Name.ToLower() && m.Surname.ToLower() == Model.Surname.ToLower());

        if(producerInDb is not null)
            throw new InvalidOperationException("ProducerNameSurname: " + Model.Name+" "+Model.Surname + " already exists.");

        var newProducer = mapper.Map<Producer>(Model);

        context.Producers.Add(newProducer);
        context.SaveChanges();
    }
}

public class CreateProducerModel 
{
        public string Name { get; set; }
        public string Surname { get; set; }
}