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
            PublishedDate = "1925-04-10",
            Owner = "John Smith",
            Status = "available",
            Borrower = ""
            },
            new Book
            {
            Id = 2,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            Isbn = "978-0-06-112008-4",
            PublishedDate = "1960-07-11",
            Owner = "Sarah Johnson",
            Status = "borrowed",
            Borrower = "Mike Davis"
            },
            new Book
            {
            Id = 3,
            Title = "1984",
            Author = "George Orwell",
            Isbn = "978-0-452-28423-4",
            PublishedDate = "1949-06-08",
            Owner = "New Guy",
            Status = "available",
            Borrower = ""
            },
            new Book
            {
            Id = 4,
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            Isbn = "978-0-14-143951-8",
            PublishedDate = "1813-01-28",
            Owner = "Emily Brown",
            Status = "available",
            Borrower = ""
            },
            new Book
            {
            Id = 5,
            Title = "The Catcher in the Rye",
            Author = "J.D. Salinger",
            Isbn = "978-0-316-76948-0",
            PublishedDate = "1951-07-16",
            Owner = "New Guy",
            Status = "borrowed",
            Borrower = "Lisa Wilson"
            },
            new Book
            {
            Id = 6,
            Title = "A Great Book",
            Author = "Johny Writer",
            Isbn = "901-0-7432-7356-5",
            PublishedDate = "1925-04-10",
            Owner = "John Smith",
            Status = "available",
            Borrower = ""
            },
            new Book
            {
            Id = 7,
            Title = "Another Average Book",
            Author = "Jane Writer",
            Isbn = "901-0-06-112008-4",
            PublishedDate = "1960-07-11",
            Owner = "Sarah Johnson",
            Status = "borrowed",
            Borrower = "Mike Davis"
            },
            new Book
            {
            Id = 8,
            Title = "Pyramids",
            Author = "Terry Pratchett",
            Isbn = "901-0-452-28423-4",
            PublishedDate = "1949-06-08",
            Owner = "New Guy",
            Status = "available",
            Borrower = ""
            },
            new Book
            {
            Id = 9,
            Title = "Lords and Ladies",
            Author = "Terry Pratchett",
            Isbn = "901-0-14-143951-8",
            PublishedDate = "1813-01-28",
            Owner = "Emily Brown",
            Status = "available",
            Borrower = ""
            },
            new Book
            {
            Id = 10,
            Title = "The Hitch Hiker's Guide to the Galaxy",
            Author = "Douglas Adams",
            Isbn = "901-0-316-76948-0",
            PublishedDate = "1951-07-16",
            Owner = "New Guy",
            Status = "borrowed",
            Borrower = "Lisa Wilson"
            }
        };
    }
}