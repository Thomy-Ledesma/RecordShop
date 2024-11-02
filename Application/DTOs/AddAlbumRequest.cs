
namespace Application.DTOs
{
    public class AddAlbumRequest
    {
        public string Name { get; set; } = string.Empty;
        public string band { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Amount { get; set; }
        public float Price { get; set; }
    }
}
