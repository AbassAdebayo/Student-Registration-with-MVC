using StudentRegistration.DTOs;
using StudentRegistration.Entities;

namespace StudentRegistration.Interfaces.Services;

public interface IStudentService
{
    public Task<BaseResponse<bool>> CreateStudent(Guid departmentId, CreateStudentRequestModel model);
    
    public Task<BaseResponse<bool>> StudentExistsByEmail(string email);
    
    public Task<BaseResponse<bool>> DeleteStudent(Guid studentId);
    
    Task<BaseResponse<Student>> EditStudent(Guid studentId, UpdateStudentRequestModel model);
    
    public Task<BaseResponse<StudentDto>> GetStudentById(Guid studentId);
    
    public Task<BaseResponse<IList<StudentDto>>> GetAllStudents();
    
    public Task<BaseResponse<IList<StudentDto>>> GetStudentsByDepartmentId(Guid departmentId);
}