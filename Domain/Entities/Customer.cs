using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : User
    {
        public List<Sale> Sales {  get; set; } = new List<Sale>();
    }
}
