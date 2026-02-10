using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace LibraryAPI.Models;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Book>(entity =>
      {
        entity.ToTable("books");
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Title).HasColumnName("title");
        entity.Property(e => e.Author).HasColumnName("author");
        entity.Property(e => e.Isbn).HasColumnName("isbn");
        entity.Property(e => e.PublishedDate).HasColumnName("publisheddate");
        entity.Property(e => e.Owner).HasColumnName("owner");
        entity.Property(e => e.Borrower).HasColumnName("borrower");
        entity.Property(e => e.Version).HasColumnName("version")
          .IsConcurrencyToken();
      });
    }

  public DbSet<Book> Books { get; set; } = null!;
}
