using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        private readonly bool isTestingEnvironment;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTestingEnvironment = false) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {
            this.isTestingEnvironment = isTestingEnvironment;
        }

    }

    
}
