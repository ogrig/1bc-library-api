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
}

