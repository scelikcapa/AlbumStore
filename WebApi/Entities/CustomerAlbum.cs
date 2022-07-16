using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class CustomerAlbum
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }     
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int AlbumId { get; set; }        
        public Album Album { get; set; }

        
    }
}