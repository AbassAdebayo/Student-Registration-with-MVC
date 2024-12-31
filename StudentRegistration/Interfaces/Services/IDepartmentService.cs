using StudentRegistration.DTOs;
using StudentRegistration.Entities;

namespace StudentRegistration.Interfaces.Services;

public interface IDepartmentService
{
    public Task<BaseResponse<bool>> CreateDepartment(CreateDepartmentRequestModel model);
    
    public Task<BaseResponse<bool>> DeleteDepartment(Guid departmentId);
    
    Task<BaseResponse<Department>> EditDepartment(Guid departmentId, UpdateDepartmentRequestModel model);
    
    public Task<BaseResponse<Department>> GetDepartmentById(Guid departmentId);
    
    public Task<BaseResponse<IList<DepartmentDto>>> GetAllDepartments();
}