using FluentValidation;
using LibraryAPI.Models;

namespace LibraryAPI.Validators
{
  public sealed class BookValidator : AbstractValidator<Book>
  {

    private static readonly string[] validStatuses = new[] { "available", "borrowed" };
    private static readonly Dictionary<string, string> invalidStatusBorrowerMessages = new Dictionary<string, string>()
    {
      { "available", "If Status is 'available', the Borrower Name must be empty." },
      { "borrowed", "If Status is 'borrowed', the Borrower Name must not be empty." }
    };

    public BookValidator()
    {
      RuleFor(book => book.Title)
          .NotEmpty()
            .WithMessage("Title is required.");
      RuleFor(book => book.Author)
          .NotEmpty()
            .WithMessage("Author is required.");
      RuleFor(book => book.Owner)
          .NotEmpty()
            .WithMessage("Owner is required.");
      RuleFor(book => book.Isbn)
          .NotEmpty()
            .WithMessage("ISBN is required.");
      RuleFor(book => book.PublishedDate)
          .NotEmpty()
            .WithMessage("Published Date is required.");
    }

    private bool IsValidStatus(string status)
    {
      return validStatuses.Contains(status);
    }

    private bool IsBorrowrValidForStatus(string status, string borrower)
    {
      switch (status)
      {
        case "borrowed":
          return !string.IsNullOrWhiteSpace(borrower);
        case "available":
          return string.IsNullOrWhiteSpace(borrower);
        default:
          return false;
      }
    }
  }
}
