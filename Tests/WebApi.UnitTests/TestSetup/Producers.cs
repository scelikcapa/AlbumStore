using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Producers
{
    public static void CreateProducers(this AlbumStoreDbContext context)
    {
        context.Producers.AddRange(
            new Producer{
                Name = "Dr.",
                Surname = "Dre"
            },
            new Producer{
                Name = "Quincy",
                Surname = "Jones"
            }        
        );

        context.SaveChanges();
    }
}