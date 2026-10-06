namespace BookTracker.Api.DTOs;

// What the API sends back
public record BookDto(
    int Id,
    string ExternalId,
    string Title,
    string? Author,
    int CurrentPage,
    int? TotalPages,
    bool IsFinished,
    DateTime DateAdded,
    DateTime? DateFinished,
    int EntryCount);

// What the front end sends to add a book
public record CreateBookDto(
    string ExternalId,
    string Title,
    string? Author,
    int? TotalPages);

// What the front end sends to update a book
public record UpdateBookDto(
    int CurrentPage,
    int? TotalPages,
    bool IsFinished);