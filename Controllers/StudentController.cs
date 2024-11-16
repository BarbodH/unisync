using Microsoft.AspNetCore.Mvc;
using UniSyncApi.Dtos;
using UniSyncApi.Repositories.Implementations;
using UniSyncApi.Services.Interfaces;

namespace UniSyncApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpPost]
    public IActionResult CreateStudent([FromBody] StudentDto student)
    {
        studentService.Create(student);
        return Created();
    }
}