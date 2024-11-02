using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{

    public class Sale
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int CustomerId { get; set; }
            public State SaleState { get; set; } = State.InProcess;
            public float Total { get; set; }

            public List<SaleAlbum> SaleAlbums { get; set; } = new List<SaleAlbum>();
    }
    }

