using Microsoft.EntityFrameworkCore;
using StudentRegistration.Entities;
using StudentRegistration.Interfaces.Repositories;
using StudentRegistration.StudentDbContext;

namespace StudentRegistration.Implementations.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentContext _studentContext;

    public StudentRepository(StudentContext studentContext)
    {
        _studentContext = studentContext;
    }
    
    public async Task<Student> CreateStudent(Student student)
    {
        await _studentContext.Students.AddAsync(student);
        await _studentContext.SaveChangesAsync();
        
        return student;

    }

    public async Task<bool> StudentExistsByEmail(string email)
    {
        return await _studentContext.Students.AnyAsync(std => std.Email == email);
    }

    public async Task<bool> DeleteStudent(Student student)
    {
        _studentContext.Students.Remove(student);
        await _studentContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<Student> EditStudent(Student student)
    {
        _studentContext.Students.Update(student);
        await _studentContext.SaveChangesAsync();
        
        return student;
    }

    public async Task<Student> GetStudentById(Guid studentId)
    {
        return await _studentContext.Students.FindAsync(studentId);
        
    }

    public async Task<IList<Student>> GetAllStudents()
    {
        var students = await _studentContext.Students
            .Include(std => std.Department)
            .AsNoTracking()
            .ToListAsync();

        return students;
    }

    public async Task<IList<Student>> GetStudentsByDepartment(Guid departmentId)
    {
        var students = await _studentContext.Students
            .Where(std => std.DepartmentId == departmentId)
            .Include(std => std.Department)
            .AsTracking()
            .ToListAsync();

        return students;
    }
}