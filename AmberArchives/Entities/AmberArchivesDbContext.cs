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
                .Property(b => b.OriginalTitle)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Edition>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Edition>()
                .Property(e => e.BookId)
                .IsRequired();

            modelBuilder.Entity<Edition>()
                .Property(e => e.Title)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(a => a.FirstName)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(a => a.LastName)
                .IsRequired();
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder.UseSqlServer(_connectionString);
		}
	}
}
