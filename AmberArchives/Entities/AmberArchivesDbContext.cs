using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Entities
{
    public class AmberArchivesDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=AmberArchivesDb;Trusted_Connection=True;";
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Edition> Editions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Book>()
                .Property(r => r.OriginalTitle)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Edition>()
                .Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Author>()
                .Property(r => r.FirstName)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(r => r.LastName)
                .IsRequired();

        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder.UseSqlServer(_connectionString);
		}
	}
}
