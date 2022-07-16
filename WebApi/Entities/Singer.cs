using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Singer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Album>? Albums { get; set; }
    }
}