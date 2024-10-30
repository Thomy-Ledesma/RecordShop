using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sale
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public List<Album> Products { get; set; } = new List<Album>();

            public int CustomerId { get; set; }
            public State SaleState { get; set; } = State.InProcess;
            public float Total { get; set; }
        }
    }

    public enum State
    {
        InProcess,
        Done,
    }

