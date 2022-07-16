using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Albums 
{
    public static void CreateAlbums(this AlbumStoreDbContext context)
    {
        context.Albums.AddRange(
                new Album{
                    Title = "The Slim Shady LP",
                    Year = new DateTime(1999,01,01),
                    Price = 10,
                    GenreId = 1,
                    ProducerId = 1,
                    Singers = context.Singers.Where(a=>a.Id == 1).ToList()
                },
                new Album{
                    Title = "Thriller",
                    Year = new DateTime(1982,01,01),
                    Price = 20,
                    GenreId = 2,
                    ProducerId = 2,
                    Singers = context.Singers.Where(a=> a.Id == 2).ToList()
                }
            );

        context.SaveChanges();
    }
}