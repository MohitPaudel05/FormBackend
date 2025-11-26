using AutoMapper;
using FormBackend.DTOs;
using FormBackend.Models;
using FormBackend.Services;
using FormBackend.Unit_Of_Work;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // POST: api/Student
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentFullDto studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _studentService.AddStudentAsync(studentDto);
            return Ok(new { message = "Student added successfully!" });
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound(new { message = "Student not found." });

            return Ok(student);
        }
    }
}