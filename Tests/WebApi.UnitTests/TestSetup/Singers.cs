using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Singers 
{
    public static void CreateSingers(this AlbumStoreDbContext context)
    {
        context.Singers.AddRange(
            new Singer{
                Name = "Marshall Bruce",
                Surname = "Mathers III"
            },
            new Singer{
                Name = "Michael",
                Surname = "Jackson"
            }
        );

        context.SaveChanges();
    }
}