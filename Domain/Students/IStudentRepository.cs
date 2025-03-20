namespace OnlineSchool.Domain.Students;

using OnlineSchool.Domain.Students.Entities;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid studentId);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
}