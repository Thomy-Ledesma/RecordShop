using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Band { get; set; }
        public string genre { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
    }
}
