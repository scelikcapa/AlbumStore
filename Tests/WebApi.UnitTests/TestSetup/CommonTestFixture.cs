using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UnitTests.TestSetup;

public class CommonTestFixture 
{
    public AlbumStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        //Context.Database.EnsureDeleted();
        var options = new DbContextOptionsBuilder<AlbumStoreDbContext>().UseInMemoryDatabase("AlbumStoreTestDb").Options;
        Context = new AlbumStoreDbContext(options);
        Context.Database.EnsureCreated();
        Context.CreateGenres();
        Context.CreateProducers();
        Context.CreateSingers();
        Context.CreateCustomers();
        Context.CreateAlbums();
        Context.CreateCustomerAlbums();

        Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}