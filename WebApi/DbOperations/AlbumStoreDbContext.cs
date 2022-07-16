using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations;

public class AlbumStoreDbContext : DbContext, IAlbumStoreDbContext
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Singer> Singers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<CustomerAlbum> CustomerAlbums { get; set; }

    public AlbumStoreDbContext(DbContextOptions<AlbumStoreDbContext> options) : base (options)
    {
        
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // IF YOU NEED TO DIRECT RELATION BETWEEN MANY-TO-MANY TABLES, YOU HAVE TO USE FLUENTAPI. INDIRECT RELATION CAN BE SETUP WITHOUT FLUENTAPI
        //   modelBuilder.Entity<Customer>()
        //     .HasMany(p => p.Albums)
        //     .WithMany(p => p.Customers)
        //     .UsingEntity<CustomerAlbum>(
        //         j => j
        //             .HasOne(pt => pt.Album)
        //             .WithMany(t => t.CustomerAlbums)
        //             .HasForeignKey(pt => pt.AlbumId),
        //         j => j
        //             .HasOne(pt => pt.Customer)
        //             .WithMany(p => p.CustomerAlbums)
        //             .HasForeignKey(pt => pt.CustomerId),
        //         j =>
        //         {
        //             j.HasKey(pt=> pt.Id);
        //             j.Property(pt => pt.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        //         });
    }

}