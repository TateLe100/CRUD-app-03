namespace BookTracker.Api.Entities;

public class ReadingEntry
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public string Summary { get; set; } = string.Empty;

    public DateTime DateRead { get; set; } = DateTime.UtcNow;
}