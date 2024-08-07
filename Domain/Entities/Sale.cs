using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Sale
    {
        public int Id { get; set; }
        public List<Album> Products { get; set; }
        public int CustomerId { get; set; }
        public bool State {  get; set; }
        public float Total {  get; set; }
    }
}
