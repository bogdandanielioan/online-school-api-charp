namespace OnlineSchool.Infrastructure.Persistence;

using OnlineSchool.Domain.Books;
using OnlineSchool.Domain.Books.Entities;
using Microsoft.EntityFrameworkCore;

public class EfBookRepository : IBookRepository
{
    private readonly OnlineSchoolDbContext _context;

    public EfBookRepository(OnlineSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdAsync(Guid bookId)
    {
        // Eager-load associated Student
        return await _context.Books
            .Include(b => b.Student)
            .FirstOrDefaultAsync(b => b.BookId == bookId);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }
}