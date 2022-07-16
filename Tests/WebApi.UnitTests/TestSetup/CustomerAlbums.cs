using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class CustomerAlbums 
{
    public static void CreateCustomerAlbums(this AlbumStoreDbContext context)
    {
         context.CustomerAlbums.AddRange(
                new CustomerAlbum{
                    CustomerId = 1,
                    AlbumId = 1,
                    Price = 10,
                    OrderDate = DateTime.Now
                },
                new CustomerAlbum{
                    CustomerId = 2,
                    AlbumId = 2,
                    Price = 20,
                    OrderDate = DateTime.Now
                }
            );

        context.SaveChanges();
    }
}