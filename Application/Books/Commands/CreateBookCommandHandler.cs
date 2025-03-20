using online_school_api.Books.ValueObjects;

namespace OnlineSchool.Application.Books.Commands;

using OnlineSchool.Domain.Books;
using OnlineSchool.Domain.Books.Entities;
using OnlineSchool.Domain.Students;

public class CreateBookCommandHandler
{
    private readonly IBookRepository _bookRepository;
    private readonly IStudentRepository _studentRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository, IStudentRepository studentRepository)
    {
        _bookRepository = bookRepository;
        _studentRepository = studentRepository;
    }

    public async Task Handle(CreateBookCommand command)
    {
        // Find the student who will manage the new book
        var student = await _studentRepository.GetByIdAsync(command.StudentId);
        if (student == null)
        {
            throw new Exception("Student not found.");
        }

        // Create the Book, linking it to the existing Student
        var book = new Book(
            command.Title,
            command.Author,
            new ISBN(command.Isbn),
            student
        );

        // Add the book to the student's list as well (domain model sync)
        student.AddBook(book);

        // Persist
        await _bookRepository.AddAsync(book);
        await _studentRepository.UpdateAsync(student);
    }
}