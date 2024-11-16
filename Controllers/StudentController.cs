using Microsoft.AspNetCore.Mvc;
using UniSyncApi.Dtos;
using UniSyncApi.Repositories.Implementations;

namespace UniSyncApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(StudentRepository studentRepository) : ControllerBase
{
    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return studentRepository.TestConnection();
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] StudentDto student)
    {
        if (studentRepository.IsDuplicate(student.Email))
        {
            return BadRequest(new { message = "Duplicate email." });
        }

        var programId = studentRepository.GetProgramId(student.Program);
        if (programId == null)
        {
            return BadRequest(new { message = "Program not found." });
        }

        if (!studentRepository.CreateStudent(student.FirstName, student.LastName, student.Email, programId.Value))
        {
            return BadRequest(new { message = "Could not create student." });
        }
        
        return Created();
    }
}