using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.SingerOperations.Commands.CreateSinger;

public class CreateSingerCommand 
{
    private readonly IAlbumStoreDbContext context;
    private readonly IMapper mapper;
    public CreateSingerModel Model { get; set; }
    

    public CreateSingerCommand(IAlbumStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var singerInDb = context.Singers.SingleOrDefault(m=>m.Name.ToLower() == Model.Name.ToLower() && m.Surname.ToLower() == Model.Surname.ToLower());

        if(singerInDb is not null)
            throw new InvalidOperationException("SingerNameSurname: " + Model.Name+" "+Model.Surname + " already exists.");

        var newSinger = mapper.Map<Singer>(Model);

        context.Singers.Add(newSinger);
        context.SaveChanges();
    }
}

public class CreateSingerModel 
{
        public string Name { get; set; }
        public string Surname { get; set; }
}