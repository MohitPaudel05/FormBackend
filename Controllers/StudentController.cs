using FormBackend.DTOs;
using FormBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // POST: api/Student
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromForm] StudentFullDto studentDto)
        {
            if (studentDto == null || studentDto.Student == null)
                return BadRequest("Student data is required.");

            var result = await _studentService.AddStudentAsync(studentDto);
            return Ok(result);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            return Ok(student);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var exists = await _studentService.GetStudentByIdAsync(id);
            if (exists == null)
                return NotFound($"Student with ID {id} not found.");

            var deleted = await _studentService.DeleteStudentAsync(id);

            if (!deleted)
                return StatusCode(500, "Failed to delete the student.");

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }
    }
}

