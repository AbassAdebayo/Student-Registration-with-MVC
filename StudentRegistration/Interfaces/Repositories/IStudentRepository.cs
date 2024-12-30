using System.Collections;
using StudentRegistration.Entities;

namespace StudentRegistration.Interfaces.Repositories;

public interface IStudentRepository
{
    public Task<Student> CreateStudent(Student student);
    public Task<bool> StudentExistsByEmail(string email);
    public Task<bool> DeleteStudent(Student student);
    Task<Student> EditStudent(Student student);
    public Task<Student> GetStudentById(Guid studentId);
    public Task<IList<Student>> GetAllStudents();
    public Task<IList<Student>> GetStudentsByDepartmentId(Guid departmentId);
}