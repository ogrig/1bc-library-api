namespace LibraryAPI.Models
{
    public record BookAvailabilityDTO(
        long Id,
        string Borrower,
        long Version
    );
}
