using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public interface IAlbumStoreDbContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Producer> Producers { get; set; }
        DbSet<Singer> Singers { get; set; }
        DbSet<Genre> Genres { get; set; }

        int SaveChanges();
    }
}