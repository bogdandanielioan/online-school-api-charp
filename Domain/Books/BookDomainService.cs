
using OnlineSchool.Domain.Books;
using OnlineSchool.Domain.Books.Entities;

namespace online_school_api.Books;

public class BookDomainService
{
    public bool CanBorrowBook(Book book)
    {
        // Business rules for borrowing books can be added here
        return true;
    }
}