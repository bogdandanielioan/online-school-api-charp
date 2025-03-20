namespace OnlineSchool.Domain.Books;

using OnlineSchool.Domain.Books.Entities;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid bookId);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
}