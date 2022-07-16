using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Album 
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get ; set; }
    public double Price { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    
    public int ProducerId { get; set; }
    public Producer Producer { get; set; }

    // Use FluentApi for direct relation. Without FluentApi this will be null
    //public ICollection<Customer> Customers { get; set; }

    public ICollection<Singer> Singers { get; set; }
    public List<CustomerAlbum> CustomerAlbums { get; set; }
    public bool IsActive { get; set; } = true; 
}