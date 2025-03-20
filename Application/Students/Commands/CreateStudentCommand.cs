namespace OnlineSchool.Application.Students.Commands;

public record CreateStudentCommand(string FirstName, string LastName, string Email);