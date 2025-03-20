using Microsoft.AspNetCore.Mvc;
using OnlineSchool.Application.Books.Commands;

namespace OnlineSchool.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly CreateBookCommandHandler _createBookHandler;

    public BooksController(CreateBookCommandHandler createBookHandler)
    {
        _createBookHandler = createBookHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        await _createBookHandler.Handle(command);
        return Ok("Book created and linked to the student successfully.");
    }
}
