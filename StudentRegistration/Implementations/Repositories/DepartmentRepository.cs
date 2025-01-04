using Microsoft.EntityFrameworkCore;
using StudentRegistration.Entities;
using StudentRegistration.Interfaces.Repositories;
using StudentRegistration.StudentDbContext;

namespace StudentRegistration.Implementations.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly StudentContext _studentContext;

    public DepartmentRepository(StudentContext studentContext)
    {
        _studentContext = studentContext;
    }
    public async Task<Department> CreateDepartment(Department department)
    {
        await _studentContext.Departments.AddAsync(department);
        await _studentContext.SaveChangesAsync();

        return department;
    }

    public async Task<bool> DeleteDepartment(Department department)
    {
        _studentContext.Departments.Remove(department);
        await _studentContext.SaveChangesAsync();

        return true;
    }

    public async Task<Department> EditDepartment(Department department)
    {
        _studentContext.Departments.Update(department);
        await _studentContext.SaveChangesAsync();

        return department;
    }

    public async Task<Department> GetDepartmentById(Guid departmentId)
    {
        return await _studentContext.Departments.FindAsync(departmentId);
    }

    public async Task<IList<Department>> GetAllDepartments()
    {
        var departmentsWithStudents = await _studentContext.Departments
            .Include(dpt => dpt.Students)
            .AsTracking()
            .ToListAsync();

        return departmentsWithStudents;
    }

    public async Task<bool> DepartmentExistsByName(string departmentName)
    {
        return await _studentContext.Departments.AnyAsync(dpt => dpt.DepartmentName == departmentName);
    }
}