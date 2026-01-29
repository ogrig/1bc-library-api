using LibraryAPI.Controllers;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Tests;

public class BooksControllerTests
{
    private static BookContext CreateInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new BookContext(options);
    }

    [Fact]
    public async Task GetBooks_EmptyDb_ReturnsEmptyList()
    {
        await using var context = CreateInMemoryContext(nameof(GetBooks_EmptyDb_ReturnsEmptyList));
        var controller = new BooksController(context);

        var result = await controller.GetBooks();

        var books = Assert.IsAssignableFrom<IEnumerable<Book>>(result.Value);
        Assert.Empty(books);
    }

    [Fact]
    public async Task GetBook_MissingId_ReturnsNotFound()
    {
        await using var context = CreateInMemoryContext(nameof(GetBook_MissingId_ReturnsNotFound));
        var controller = new BooksController(context);

        var result = await controller.GetBook(123);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostBooks_PersistsAndReturnsCreatedAtAction()
    {
        await using var context = CreateInMemoryContext(nameof(PostBooks_PersistsAndReturnsCreatedAtAction));
        var controller = new BooksController(context);

        var book = new Book
        {
            Title = "New Book",
            Author = "Author",
            Isbn = "ISBN",
            PublishedDate = "2026-01-01",
            Owner = "Owner",
            Status = "available",
            Borrower = ""
        };

        var result = await controller.PostBooks(book);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(BooksController.GetBook), created.ActionName);

        Assert.Equal(1, await context.Books.CountAsync());
        Assert.True(book.Id != 0);
    }

    [Fact]
    public async Task PutBook_IdMismatch_ReturnsBadRequest()
    {
        await using var context = CreateInMemoryContext(nameof(PutBook_IdMismatch_ReturnsBadRequest));
        var controller = new BooksController(context);

        var result = await controller.PutBook(1, new Book { Id = 2 });

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutBook_ExistingBook_ReturnsNoContentAndUpdates()
    {
        var dbName = nameof(PutBook_ExistingBook_ReturnsNoContentAndUpdates);
        await using var context = CreateInMemoryContext(dbName);
        context.Books.Add(new Book { Id = 1, Title = "Old", Author = "A" });
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var controller = new BooksController(context);

        var updated = new Book { Id = 1, Title = "New", Author = "A" };
        var result = await controller.PutBook(1, updated);

        Assert.IsType<NoContentResult>(result);

        var fromDb = await context.Books.FindAsync(1L);
        Assert.NotNull(fromDb);
        Assert.Equal("New", fromDb!.Title);
    }

    [Fact]
    public async Task DeleteBook_MissingId_ReturnsNotFound()
    {
        await using var context = CreateInMemoryContext(nameof(DeleteBook_MissingId_ReturnsNotFound));
        var controller = new BooksController(context);

        var result = await controller.DeleteBook(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteBook_ExistingId_ReturnsNoContentAndDeletes()
    {
        var dbName = nameof(DeleteBook_ExistingId_ReturnsNoContentAndDeletes);
        await using var context = CreateInMemoryContext(dbName);
        context.Books.Add(new Book { Id = 1, Title = "T" });
        await context.SaveChangesAsync();

        var controller = new BooksController(context);

        var result = await controller.DeleteBook(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Equal(0, await context.Books.CountAsync());
    }

    // EF Core InMemory doesn't reliably produce DbUpdateConcurrencyException on "update missing row",
    // so we exercise the controller's concurrency logic using a context that forces the exception.
    [Fact]
    public async Task PutBook_WhenSaveThrowsConcurrencyAndBookMissing_ReturnsNotFound()
    {
        var options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(nameof(PutBook_WhenSaveThrowsConcurrencyAndBookMissing_ReturnsNotFound))
            .Options;

        await using var context = new ConcurrencyThrowingBookContext(options);
        var controller = new BooksController(context);

        var result = await controller.PutBook(1, new Book { Id = 1, Title = "X" });

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutBook_WhenSaveThrowsConcurrencyAndBookExists_Rethrows()
    {
        var options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(nameof(PutBook_WhenSaveThrowsConcurrencyAndBookExists_Rethrows))
            .Options;

        await using var context = new ConcurrencyThrowingBookContext(options);
        context.Books.Add(new Book { Id = 1, Title = "Existing" });
        await context.SaveChangesAsync(acceptAllChangesOnSuccess: true);
        context.ChangeTracker.Clear();

        var controller = new BooksController(context);

        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() =>
            controller.PutBook(1, new Book { Id = 1, Title = "Updated" }));
    }

    private sealed class ConcurrencyThrowingBookContext(DbContextOptions<BookContext> options) : BookContext(options)
    {
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new DbUpdateConcurrencyException("Simulated concurrency exception.");
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Allow seeding to succeed in the "BookExists == true" test.
            if (acceptAllChangesOnSuccess)
            {
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }

            throw new DbUpdateConcurrencyException("Simulated concurrency exception.");
        }
    }
}

