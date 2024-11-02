using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Band { get; set; }
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }
        public int Amount { get; set; } = 0;
        [Required]
        public float Price { get; set; }
        public List<SaleAlbum> SaleAlbums { get; set; } = new List<SaleAlbum>();
    }
}
