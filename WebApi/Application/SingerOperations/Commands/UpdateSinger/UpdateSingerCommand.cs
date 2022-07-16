using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.SingerOperations.Commands.UpdateSinger;

public class UpdateSingerCommand 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    
    public int SingerId { get; set; }
    public UpdateSingerModel Model { get; set; }
    

    public UpdateSingerCommand(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var singerInDb = context.Singers.SingleOrDefault(m=>m.Id == SingerId);

        if(singerInDb is null)
            throw new InvalidOperationException("SingerId: "+SingerId+" does not exist.");
        
        bool isSameNameExists = context.Singers.Where(m=>
                                                m.Name.ToLower() == (Model.Name == null ? singerInDb.Name.ToLower() : Model.Name.ToLower()) && 
                                                    m.Surname.ToLower() == (Model.Surname == null ? singerInDb.Surname.ToLower() : Model.Surname.ToLower()) && 
                                                m.Id != SingerId)
                                              .Any();
        
        if(isSameNameExists)
            throw new InvalidOperationException("SingerNameSurname: "+ Model.Name+" "+Model.Surname+" already exists, choose another name.");

        mapper.Map<UpdateSingerModel, Singer>(Model, singerInDb);

        context.SaveChanges();
    }
}

public class UpdateSingerModel 
{
    public string? Name { get; set; }
    public string? Surname { get; set; }    
}