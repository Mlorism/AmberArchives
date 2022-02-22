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
		public AmberArchivesDbContext(DbContextOptions<AmberArchivesDbContext> options) : base(options)
		{

		}
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }
        public DbSet<Genere> Generes { get; set; }


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

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<UserRole>()
                .Property(u => u.RoleType)
                .IsRequired();
        }
        
    }
}
