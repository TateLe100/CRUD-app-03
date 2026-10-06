using BookTracker.Api.Data;
using BookTracker.Api.DTOs;
using BookTracker.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        var books = await _context.Books
            .OrderByDescending(b => b.DateAdded)
            .Select(b => new BookDto(
                b.Id, b.ExternalId, b.Title, b.Author,
                b.CurrentPage, b.TotalPages, b.IsFinished,
                b.DateAdded, b.DateFinished,
                b.ReadingEntries.Count))
            .ToListAsync();

        return Ok(books);
    }

    // GET: api/books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await _context.Books
            .Where(b => b.Id == id)
            .Select(b => new BookDto(
                b.Id, b.ExternalId, b.Title, b.Author,
                b.CurrentPage, b.TotalPages, b.IsFinished,
                b.DateAdded, b.DateFinished,
                b.ReadingEntries.Count))
            .FirstOrDefaultAsync();

        if (book is null) return NotFound();

        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto dto)
    {
        var exists = await _context.Books.AnyAsync(b => b.ExternalId == dto.ExternalId);
        if (exists) return Conflict("This book is already in your list.");

        var book = new Book
        {
            ExternalId = dto.ExternalId,
            Title = dto.Title,
            Author = dto.Author,
            TotalPages = dto.TotalPages
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var result = new BookDto(
            book.Id, book.ExternalId, book.Title, book.Author,
            book.CurrentPage, book.TotalPages, book.IsFinished,
            book.DateAdded, book.DateFinished, 0);

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, result);
    }

    // PUT: api/books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) return NotFound();

        book.CurrentPage = dto.CurrentPage;
        book.TotalPages = dto.TotalPages;

        // Only stamp the finish date when it flips from unfinished to finished
        if (dto.IsFinished && !book.IsFinished)
            book.DateFinished = DateTime.UtcNow;
        else if (!dto.IsFinished)
            book.DateFinished = null;

        book.IsFinished = dto.IsFinished;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null) return NotFound();

        _context.Books.Remove(book); // cascade deletes its reading entries
        await _context.SaveChangesAsync();
        return NoContent();
    }
}