using StudentRegistration.Entities;

namespace StudentRegistration.Interfaces.Repositories;

public interface IDepartmentRepository
{
    public Task<Department> CreateDepartment(Department department);
    public Task<bool> DeleteDepartment(Department department);
    Task<Department> EditDepartment(Department department);
    public Task<Department> GetDepartmentById(Guid departmentId);
    public Task<IList<Department>> GetAllDepartments();
    public Task<bool> DepartmentExistsByName(string departmentName);
}