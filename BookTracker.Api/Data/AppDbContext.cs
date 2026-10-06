using BookTracker.Api.Entities;

namespace BookTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<ReadingEntry>  ReadingEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasMany(b => b.ReadingEntries)
            .WithOne(e => e.Book)
            .HasForeignKey(e => e.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ExternalId)
            .IsUnique(); // prevents adding the same book twice
    }
}