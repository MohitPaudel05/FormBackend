using FormBackend.DTOs;

namespace FormBackend.Services
{
    public interface IStudentService
    {
        Task<StudentFullDto> AddStudentAsync(StudentFullDto studentDto);
        Task<StudentFullDto?> GetStudentByIdAsync(int studentId);

        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<bool> DeleteStudentAsync(int id);

    }
}
