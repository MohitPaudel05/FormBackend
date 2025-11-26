using FormBackend.DTOs;
using FormBackend.Models;

namespace FormBackend.Services
{
    public interface IStudentService
    {
        Task AddStudentAsync(StudentFullDto studentDto);
        Task<StudentFullDto> GetStudentByIdAsync(int studentId);

    }
}   
