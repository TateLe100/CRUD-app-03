namespace BookTracker.Api.Entities;

public class Book
{
    public int Id { get; set; }

    // ID from the external API (Google Books, Open Library, etc.)
    public string ExternalId { get; set; } = string.Empty;

    // Cached for display so lists don't need an external call
    public string Title { get; set; } = string.Empty;
    public string? Author { get; set; }

    public int CurrentPage { get; set; }
    public int? TotalPages { get; set; }

    public bool IsFinished { get; set; }
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    public DateTime? DateFinished { get; set; }

    public List<ReadingEntry> ReadingEntries { get; set; } = new();
}