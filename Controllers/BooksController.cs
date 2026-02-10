using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Controllers
{
  [Route("Books")]
  [ApiController]
  [Authorize]
  public class BooksController : ControllerBase
  {
    private readonly BookContext _context;

    public BooksController(BookContext context)
    {
      _context = context;
    }

    // GET: Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
      return await _context.Books.OrderBy(book => book.Title).ToListAsync();
    }

    // GET: Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(long id)
    {
      var book = await _context.Books.FindAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      return book;
    }

    // PUT: Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(long id, Book book)
    {
      if (id != book.Id)
      {
        return BadRequest();
      }

      if (!TryGetBook(id, out var theBook))
      {
        return NotFound();
      }

      try
      {
        _context.Books.Attach(book);
        _context.Entry(book).Property(b => b.Borrower).IsModified = true;
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        return StatusCode(StatusCodes.Status412PreconditionFailed,
                "Record was modified by another request.");
      }

      return NoContent();
    }

    // POST: Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Book>> PostBooks(Book book, IValidator<Book> validator)
    {
      var validationResult = validator.Validate(book);

      if (!validationResult.IsValid)
      {
        return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
      }

      _context.Books.Add(book);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // DELETE: Books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(long id)
    {
      var book = await _context.Books.FindAsync(id);
      if (book == null)
      {
        return NotFound();
      }

      _context.Books.Remove(book);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool TryGetBook(long id, out Book? theBook)
    {
      theBook = _context.Books.AsNoTracking<Book>().SingleOrDefault(book => book.Id == id);

      return theBook != null;
    }
  }
}
