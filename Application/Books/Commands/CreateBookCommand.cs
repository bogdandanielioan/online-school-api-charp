namespace OnlineSchool.Application.Books.Commands;

public record CreateBookCommand(string Title, string Author, string Isbn, Guid StudentId);