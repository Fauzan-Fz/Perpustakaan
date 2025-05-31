using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Perpustakaan.Models;

namespace Perpustakaan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<KoleksiBuku> Buku { get; set; }

        public DbSet<PenulisBuku> Penulis { get; set; }

        public DbSet<GenreBuku> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<KoleksiBuku>()
                .HasOne(p => p.Penulis)
                .WithMany()
                .HasForeignKey(p => p.PenulisId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<KoleksiBuku>()
                .HasOne(p => p.Genre)
                .WithMany()
                .HasForeignKey(p => p.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}