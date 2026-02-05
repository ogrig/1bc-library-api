namespace LibraryAPI.Models;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string PublishedDate { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public string Status { get; set; } = "available";
    public string Borrower { get; set; } = string.Empty;
}
