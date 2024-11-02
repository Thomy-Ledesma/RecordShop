using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        private readonly bool isTestingEnvironment;
        public DbSet<Album> Albums { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTestingEnvironment = false) : base(options)
        {
            this.isTestingEnvironment = isTestingEnvironment;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleAlbum>()
                .HasKey(sa => new { sa.SaleId, sa.AlbumId });

            modelBuilder.Entity<SaleAlbum>()
                .HasOne(sa => sa.Sale)
                .WithMany(s => s.SaleAlbums)
                .HasForeignKey(sa => sa.SaleId);

            modelBuilder.Entity<SaleAlbum>()
                .HasOne(sa => sa.Album)
                .WithMany(a => a.SaleAlbums)
                .HasForeignKey(sa => sa.AlbumId);

            modelBuilder.Entity<Customer>()
                .Property(u => u.Role)
                .HasConversion(new EnumToStringConverter<UserRole>());
        }
    }
}
