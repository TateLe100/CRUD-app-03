namespace BookTracker.Api.DTOs;

public record ReadingEntryDto(
    int Id,
    int BookId,
    int StartPage,
    int EndPage,
    string Summary,
    DateTime DateRead);

public record CreateReadingEntryDto(
    int StartPage,
    int EndPage,
    string Summary);

public record UpdateReadingEntryDto(
    int StartPage,
    int EndPage,
    string Summary);