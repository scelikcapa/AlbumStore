using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ProducerOperations.Commands.UpdateProducer;

public class UpdateProducerCommand 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    
    public int ProducerId { get; set; }
    public UpdateProducerModel Model { get; set; }
    

    public UpdateProducerCommand(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var producerInDb = context.Producers.SingleOrDefault(m=>m.Id == ProducerId);

        if(producerInDb is null)
            throw new InvalidOperationException("ProducerId: "+ProducerId+" does not exist.");
        
        bool isSameNameExists = context.Producers.Where(m=>
                                                    m.Name.ToLower() == (Model.Name == null ? producerInDb.Name.ToLower() : Model.Name.ToLower()) && 
                                                    m.Surname.ToLower() == (Model.Surname == null ? producerInDb.Surname.ToLower() : Model.Surname.ToLower()) && 
                                                    m.Id != ProducerId)
                                                 .Any();
        
        if(isSameNameExists)
            throw new InvalidOperationException("ProducerNameSurname: "+ Model.Name+" "+Model.Surname+" already exists, choose another name.");

        mapper.Map<UpdateProducerModel, Producer>(Model, producerInDb);

        context.SaveChanges();
    }
}

public class UpdateProducerModel 
{
    public string? Name { get; set; }
    public string? Surname { get; set; }    
}