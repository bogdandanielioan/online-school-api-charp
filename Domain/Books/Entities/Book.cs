using online_school_api.Books.ValueObjects;
namespace OnlineSchool.Domain.Books.Entities;
using OnlineSchool.Domain.Students.Entities;

public class Book
{
    public Guid BookId { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public ISBN Isbn { get; private set; }

    // One-to-Many Relationship: Book belongs to a Student
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    private Book() { } // Required by EF Core

    public Book(string title, string author, ISBN isbn, Student student)
    {
        BookId   = Guid.NewGuid();
        Title    = title;
        Author   = author;
        Isbn     = isbn;

        // Link to the Student
        Student  = student ?? throw new ArgumentNullException(nameof(student));
        StudentId = student.StudentId;
    }
}