using OnlineSchool.Application.Students.Commands;

namespace OnlineSchool.API.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly CreateStudentCommandHandler _createStudentHandler;

    public StudentsController(CreateStudentCommandHandler createStudentHandler)
    {
        _createStudentHandler = createStudentHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command)
    {
        await _createStudentHandler.Handle(command);
        return Ok("Student created successfully.");
    }
}