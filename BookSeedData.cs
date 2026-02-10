using LibraryAPI.Models;

namespace LibraryAPI;

public static class BookSeedData
{
  public static List<Book> GetBooks()
  {
    return new List<Book>
        {
            new Book
            {
            Id = 1,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            Isbn = "978-0-7432-7356-5",
            PublishedDate = DateOnly.Parse("1925-04-10"),
            Owner = "John Smith",
            Borrower = ""
            },
            new Book
            {
            Id = 2,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            Isbn = "978-0-06-112008-4",
            PublishedDate = DateOnly.Parse("1960-07-11"),
            Owner = "Sarah Johnson",
            Borrower = "Mike Davis"
            },
            new Book
            {
            Id = 3,
            Title = "1984",
            Author = "George Orwell",
            Isbn = "978-0-452-28423-4",
            PublishedDate = DateOnly.Parse("1949-06-08"),
            Owner = "New Guy",
            Borrower = ""
            },
            new Book
            {
            Id = 4,
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            Isbn = "978-0-14-143951-8",
            PublishedDate = DateOnly.Parse("1813-01-28"),
            Owner = "Emily Brown",
            Borrower = ""
            },
            new Book
            {
            Id = 5,
            Title = "The Catcher in the Rye",
            Author = "J.D. Salinger",
            Isbn = "978-0-316-76948-0",
            PublishedDate = DateOnly.Parse("1951-07-16"),
            Owner = "New Guy",
            Borrower = "Lisa Wilson"
            },
            new Book
            {
            Id = 6,
            Title = "A Great Book",
            Author = "Johny Writer",
            Isbn = "901-0-7432-7356-5",
            PublishedDate = DateOnly.Parse("1925-04-10"),
            Owner = "John Smith",
            Borrower = ""
            },
            new Book
            {
            Id = 7,
            Title = "Another Average Book",
            Author = "Jane Writer",
            Isbn = "901-0-06-112008-4",
            PublishedDate = DateOnly.Parse("1960-07-11"),
            Owner = "Sarah Johnson",
            Borrower = "Mike Davis"
            },
            new Book
            {
            Id = 8,
            Title = "Pyramids",
            Author = "Terry Pratchett",
            Isbn = "901-0-452-28423-4",
            PublishedDate = DateOnly.Parse("1949-06-08"),
            Owner = "New Guy",
            Borrower = ""
            },
            new Book
            {
            Id = 9,
            Title = "Lords and Ladies",
            Author = "Terry Pratchett",
            Isbn = "901-0-14-143951-8",
            PublishedDate = DateOnly.Parse("1813-01-28"),
            Owner = "Emily Brown",
            Borrower = ""
            },
            new Book
            {
            Id = 10,
            Title = "The Hitch Hiker's Guide to the Galaxy",
            Author = "Douglas Adams",
            Isbn = "901-0-316-76948-0",
            PublishedDate = DateOnly.Parse("1951-07-16"),
            Owner = "New Guy",
            Borrower = "Lisa Wilson"
            }
        };
  }
}