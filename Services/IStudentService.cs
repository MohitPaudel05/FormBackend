using FormBackend.DTOs;

namespace FormBackend.Services
{
    public interface IStudentService
    {
        Task<StudentFullDto> AddStudentAsync(StudentFullDto studentDto);
        Task<StudentFullDto?> GetStudentByIdAsync(int studentId);
    }
}
