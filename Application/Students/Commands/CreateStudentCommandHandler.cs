using online_school_api.Students.ValueObjects;

namespace OnlineSchool.Application.Students.Commands;

using OnlineSchool.Domain.Students;
using OnlineSchool.Domain.Students.Entities;

public class CreateStudentCommandHandler
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task Handle(CreateStudentCommand command)
    {
        var student = new Student(
            command.FirstName,
            command.LastName,
            new Email(command.Email)
        );

        await _studentRepository.AddAsync(student);
    }
}