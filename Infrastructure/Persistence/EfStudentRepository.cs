namespace OnlineSchool.Infrastructure.Persistence;

using OnlineSchool.Domain.Students;
using OnlineSchool.Domain.Students.Entities;
using Microsoft.EntityFrameworkCore;

public class EfStudentRepository : IStudentRepository
{
    private readonly OnlineSchoolDbContext _context;

    public EfStudentRepository(OnlineSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetByIdAsync(Guid studentId)
    {
        // Also eager-load Books
        return await _context.Students
            .Include(s => s.Books) 
            .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }
}