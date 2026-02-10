using FluentValidation;
using LibraryAPI.Models;

namespace LibraryAPI.Validators
{
  public sealed class BookValidator : AbstractValidator<Book>
  {

    private static readonly string[] validStatuses = new[] { "available", "borrowed" };

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
  }
}
