using LibraryAPI;

namespace LibraryAPI.Tests;

public class BookSeedDataTests
{
    [Fact]
    public void GetBooks_Returns10Books_WithUniqueIds()
    {
        var books = BookSeedData.GetBooks();

        Assert.Equal(10, books.Count);
        Assert.Equal(10, books.Select(b => b.Id).Distinct().Count());
    }

    [Fact]
    public void GetBooks_BorrowerMatchesStatusConvention()
    {
        var books = BookSeedData.GetBooks();

        foreach (var b in books)
        {
            if (string.Equals(b.Status, "borrowed", StringComparison.OrdinalIgnoreCase))
            {
                Assert.False(string.IsNullOrWhiteSpace(b.Borrower));
            }

            if (string.Equals(b.Status, "available", StringComparison.OrdinalIgnoreCase))
            {
                Assert.True(string.IsNullOrWhiteSpace(b.Borrower));
            }
        }
    }
}

