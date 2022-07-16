using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Customers 
{
    public static void CreateCustomers(this AlbumStoreDbContext context)
    {
        context.Customers.AddRange(
            new Customer{
                Name = "Samet",
                Surname = "Celikcapa",
                Email = "samet@mail.com",
                Password = "password",
                Genres = context.Genres.Where(g=>g.Id == 1).ToList()
            },
            new Customer{
                Name = "Zeynep",
                Surname = "Celikcapa",
                Email = "zeynep@mail.com",
                Password = "password",
                Genres = context.Genres.Where(g=>g.Id == 2).ToList()
            } 
        );

        context.SaveChanges();
    }
}