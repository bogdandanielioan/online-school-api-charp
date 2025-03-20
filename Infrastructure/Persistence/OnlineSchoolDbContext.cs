using OnlineSchool.Domain.Books.Entities;
using OnlineSchool.Domain.Students.Entities;

namespace OnlineSchool.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

public class OnlineSchoolDbContext : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    public OnlineSchoolDbContext(DbContextOptions<OnlineSchoolDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureStudents(modelBuilder);
        ConfigureBooks(modelBuilder);
    }

    private void ConfigureStudents(ModelBuilder modelBuilder)
    {
        // Configure Student Table
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.StudentId);

            entity.Property(s => s.FirstName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(s => s.LastName)
                  .HasMaxLength(100)
                  .IsRequired();

            // Map Email as an Owned Type
            entity.OwnsOne(s => s.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("Email")
                     .IsRequired();
            });

            // One-to-Many: Student -> Books
            entity.HasMany(s => s.Books)
                  .WithOne(b => b.Student)
                  .HasForeignKey(b => b.StudentId)
                  .OnDelete(DeleteBehavior.Cascade); // If student is deleted, remove their books
        });
    }

    private void ConfigureBooks(ModelBuilder modelBuilder)
    {
        // Configure Book Table
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.BookId);

            entity.Property(b => b.Title)
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(b => b.Author)
                  .HasMaxLength(255)
                  .IsRequired();

            // Map ISBN as an Owned Type
            entity.OwnsOne(b => b.Isbn, isbn =>
            {
                isbn.Property(i => i.Value)
                    .HasColumnName("ISBN")
                    .IsRequired();
            });
        });
    }
}
