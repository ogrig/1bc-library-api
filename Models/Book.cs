using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Models;

[Table("books")]
public class Book
{
  // public string Status { get; set; } = "available";

  [Column("id")]
  public long Id { get; set; }

  [Column("title")]
  public string Title { get; set; } = string.Empty;

  [Column("author")]
  public string Author { get; set; } = string.Empty;

  [Column("isbn")]
  public string Isbn { get; set; } = string.Empty;

  [Column("publisheddate")]
  public DateOnly PublishedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

  [Column("owner")]
  public string Owner { get; set; } = string.Empty;

  [Column("borrower")]
  public string Borrower { get; set; } = string.Empty;

  [Column("version")]
  public int Version { get; set; }
}
