using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations;

public class DataGenerator 
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AlbumStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<AlbumStoreDbContext>>()))
        {
            if(context.Genres.Any() || context.Producers.Any() || context.Albums.Any())
                return;

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
}