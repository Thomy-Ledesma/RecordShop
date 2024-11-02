
namespace Domain.Entities
{
    public class SaleAlbum
    {
        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int Quantity { get; set; }
    }
}
