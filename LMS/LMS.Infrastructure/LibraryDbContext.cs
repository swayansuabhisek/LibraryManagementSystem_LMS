
using LMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options){ }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Borrower> Borrowers => Set<Borrower>();
    public DbSet<BorrowBook> BorrowBooks => Set<BorrowBook>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //author table 
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(a => a.Description)
                .HasMaxLength(200);
        });

        //book table
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.BookNumber);

            entity.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(b => b.TotalCopies)
                .IsRequired();

            entity.Property(b => b.AvailableCopies)
                .IsRequired();

            entity.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(b => b.IsAvailable)
                .IsRequired();
        });

        //borrower table
        modelBuilder.Entity<Borrower>(entity =>
        {
            entity.HasKey(bw => bw.Id);

            entity.Property(bw => bw.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(bw => bw.Email)
                .IsRequired()
                .HasMaxLength(150);
        });

        //borrow book table - for core logic 
        modelBuilder.Entity<BorrowBook>(entity =>
        {
            entity.HasKey(bb => bb.Id);

            entity.Property(bb => bb.BookIssueDate)
                .IsRequired();

            entity.Property(bb => bb.IsReturned)
                .IsRequired();

            entity.HasOne(bb => bb.Book)
                .WithMany(bb => bb.BorrowBooks)
                .HasForeignKey(bb => bb.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(bb => bb.Borrower)
                .WithMany(bb => bb.BorrowBooks)
                .HasForeignKey(bb => bb.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
