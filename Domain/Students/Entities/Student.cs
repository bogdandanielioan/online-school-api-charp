using online_school_api.Students.ValueObjects;
using OnlineSchool.Domain.Books;
using OnlineSchool.Domain.Books.Entities;

namespace OnlineSchool.Domain.Students.Entities;
public class Student
{
    public Guid StudentId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }

    // One-to-Many Relationship: A Student manages multiple Books
    private readonly List<Book> _books = new();
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    // EF Core requires a parameterless constructor for proxies/materialization
    private Student() { }

    public Student(string firstName, string lastName, Email email)
    {
        StudentId  = Guid.NewGuid();
        FirstName  = firstName;
        LastName   = lastName;
        Email      = email;
    }

    // Domain behavior to add a Book
    public void AddBook(Book book)
    {
        if (book is null)
            throw new ArgumentNullException(nameof(book));

        _books.Add(book);
    }
}