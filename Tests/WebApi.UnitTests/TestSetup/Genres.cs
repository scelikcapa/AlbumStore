using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Genres 
{
    public static void CreateGenres(this AlbumStoreDbContext context)
    {
        context.Genres.AddRange(
            new Genre{
                Name = "Hip-Hop"
            },
            new Genre{
                Name = "Pop"
            },
            new Genre{
                Name = "Rock"
            }
        );

        context.SaveChanges();
    }
}