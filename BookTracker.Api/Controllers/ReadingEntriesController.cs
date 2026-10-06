using BookTracker.Api.Data;
using BookTracker.Api.DTOs;
using BookTracker.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Api.Controllers;

[ApiController]
[Route("api/books/{bookId}/entries")]
public class ReadingEntriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReadingEntriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/books/1/entries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadingEntryDto>>> GetEntries(int bookId)
    {
        var bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
        if (!bookExists) return NotFound("Book not found.");

        var entries = await _context.ReadingEntries
            .Where(e => e.BookId == bookId)
            .OrderBy(e => e.StartPage)
            .Select(e => new ReadingEntryDto(
                e.Id, e.BookId, e.StartPage, e.EndPage, e.Summary, e.DateRead))
            .ToListAsync();

        return Ok(entries);
    }

    // GET: api/books/1/entries/3
    [HttpGet("{id}")]
    public async Task<ActionResult<ReadingEntryDto>> GetEntry(int bookId, int id)
    {
        var entry = await _context.ReadingEntries
            .Where(e => e.Id == id && e.BookId == bookId)
            .Select(e => new ReadingEntryDto(
                e.Id, e.BookId, e.StartPage, e.EndPage, e.Summary, e.DateRead))
            .FirstOrDefaultAsync();

        if (entry is null) return NotFound();

        return Ok(entry);
    }

    // POST: api/books/1/entries
    [HttpPost]
    public async Task<ActionResult<ReadingEntryDto>> CreateEntry(int bookId, CreateReadingEntryDto dto)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book is null) return NotFound("Book not found.");

        var error = ValidatePages(dto.StartPage, dto.EndPage, book.TotalPages, dto.Summary);
        if (error is not null) return BadRequest(error);

        var entry = new ReadingEntry
        {
            BookId = bookId,
            StartPage = dto.StartPage,
            EndPage = dto.EndPage,
            Summary = dto.Summary.Trim()
        };

        _context.ReadingEntries.Add(entry);
        await _context.SaveChangesAsync();

        await RecalculateCurrentPage(book);

        var result = new ReadingEntryDto(
            entry.Id, entry.BookId, entry.StartPage, entry.EndPage, entry.Summary, entry.DateRead);

        return CreatedAtAction(nameof(GetEntry), new { bookId, id = entry.Id }, result);
    }

    // PUT: api/books/1/entries/3
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEntry(int bookId, int id, UpdateReadingEntryDto dto)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book is null) return NotFound("Book not found.");

        var entry = await _context.ReadingEntries
            .FirstOrDefaultAsync(e => e.Id == id && e.BookId == bookId);
        if (entry is null) return NotFound();

        var error = ValidatePages(dto.StartPage, dto.EndPage, book.TotalPages, dto.Summary);
        if (error is not null) return BadRequest(error);

        entry.StartPage = dto.StartPage;
        entry.EndPage = dto.EndPage;
        entry.Summary = dto.Summary.Trim();

        await _context.SaveChangesAsync();
        await RecalculateCurrentPage(book);

        return NoContent();
    }

    // DELETE: api/books/1/entries/3
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEntry(int bookId, int id)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book is null) return NotFound("Book not found.");

        var entry = await _context.ReadingEntries
            .FirstOrDefaultAsync(e => e.Id == id && e.BookId == bookId);
        if (entry is null) return NotFound();

        _context.ReadingEntries.Remove(entry);
        await _context.SaveChangesAsync();
        await RecalculateCurrentPage(book);

        return NoContent();
    }

    // ---------- Helpers ----------

    private static string? ValidatePages(int startPage, int endPage, int? totalPages, string summary)
    {
        if (startPage < 1) return "Start page must be at least 1.";
        if (endPage < startPage) return "End page can't be before the start page.";
        if (totalPages.HasValue && endPage > totalPages.Value)
            return $"End page can't be past the last page ({totalPages}).";
        if (string.IsNullOrWhiteSpace(summary)) return "Summary is required.";
        return null;
    }

    // Current page = furthest page reached across all entries
    private async Task RecalculateCurrentPage(Book book)
    {
        book.CurrentPage = await _context.ReadingEntries
            .Where(e => e.BookId == book.Id)
            .MaxAsync(e => (int?)e.EndPage) ?? 0;

        await _context.SaveChangesAsync();
    }
}