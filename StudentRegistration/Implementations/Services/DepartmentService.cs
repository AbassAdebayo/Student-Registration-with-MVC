using StudentRegistration.DTOs;
using StudentRegistration.Entities;
using StudentRegistration.Interfaces.Repositories;
using StudentRegistration.Interfaces.Services;

namespace StudentRegistration.Implementations.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<BaseResponse<bool>> CreateDepartment(CreateDepartmentRequestModel model)
    {
        var department = await _departmentRepository.DepartmentExistsByName(model.DepartmentName);
        if(department)
        {
            return new BaseResponse<bool>
            {
                Message = "Department already exists!",
                Status = false
            };
        }
        var newDepartment = new Department
        {
            DepartmentName = model.DepartmentName,
            DepartmentCode = model.DepartmentCode,
            DateOfCreation = DateTime.UtcNow

        };
        var createDepartment = await _departmentRepository.CreateDepartment(newDepartment);
        if(createDepartment == null)
        {
            return new BaseResponse<bool>
            {
                Message = "Failed to create new department!",
                Status = false
            };
        }

        return new BaseResponse<bool>
        {
            Message = "Department created successfully!",
            Status = true
        };
    }

    public async Task<BaseResponse<bool>> DeleteDepartment(Guid departmentId)
    {
        var department = await _departmentRepository.GetDepartmentById(departmentId);
        if(department == null)
        {
            return new BaseResponse<bool>
            {
                Message = "Department not found!",
                Status = false
            };
        }
        var deleteDepartment = await _departmentRepository.DeleteDepartment(department);
        if(deleteDepartment == null)
        {
            return new BaseResponse<bool>
            {
                Message = "Failed to delete department!",
                Status = false
            };
        }
        return new BaseResponse<bool>
        {
            Message = "Department deleted successfully!",
            Status = true
        };
    }

    public async Task<BaseResponse<Department>> EditDepartment(Guid departmentId, UpdateDepartmentRequestModel model)
    {
        var department = await _departmentRepository.GetDepartmentById(departmentId);
        if(department == null)
        {
            return new BaseResponse<Department>
            {
                Message = "Department not found!",
                Status = false
            };
        }

        var oldDepartment = department.DepartmentName;
        
        department.DepartmentName = model.DepartmentName;
        department.DepartmentCode = model.DepartmentCode;
        
        var updateDepartment = await _departmentRepository.EditDepartment(department);
        if (updateDepartment == null)
        {
            return new BaseResponse<Department>
            {
                Message = "Failed to update department!",
                Status = false
            };
        }

        return new BaseResponse<Department>
        {
            Message = $"Department of {oldDepartment} has been successfully changed to {model.DepartmentName}!",
            Status = true
        };
    }

    public async Task<BaseResponse<Department>> GetDepartmentById(Guid departmentId)
    {
        var department = await _departmentRepository.GetDepartmentById(departmentId);
        if(department == null)
        {
            return new BaseResponse<Department>
            {
                Message = "Department not found!",
                Status = false
            };
        }

        var departmentDto = new DepartmentDto
        {
            Id = department.Id,
            DepartmentName = department.DepartmentName,
            DepartmentCode = department.DepartmentCode,
            StudentCount = department.StudentCount,
            DateOfCreation = department.DateOfCreation,
            Students = department.Students.Select(std => new StudentDto
            {
                Id = std.Id,
                Email = std.Email,
                MatricNumber = std.MatricNumber,
                DateOfCreation = std.DateOfCreation,
            }).ToList()
        };
        return new BaseResponse<Department>
        {
            Message = "Department along its students successfully fetched!",
            Status = true
        };
    }

    public async Task<BaseResponse<IList<DepartmentDto>>> GetAllDepartments()
    {
        var departments = await _departmentRepository.GetAllDepartments();
        if(departments == null || !departments.Any())
        {
            return new BaseResponse<IList<DepartmentDto>>
            {
                Message = "No departments found!",
                Status = false
            };
        }

        var departmentDtos = departments.Select(dpt => new DepartmentDto
        {
            Id = dpt.Id,
            DepartmentName = dpt.DepartmentName,
            DepartmentCode = dpt.DepartmentCode,
            StudentCount = dpt.StudentCount,
            DateOfCreation = dpt.DateOfCreation,
            Students = dpt.Students.Select(std => new StudentDto
            {
                Id = std.Id,
                Email = std.Email,
                MatricNumber = std.MatricNumber,
                DateOfCreation = std.DateOfCreation,
            }).ToList()
            
        }).ToList();

        return new BaseResponse<IList<DepartmentDto>>
        {
            Message = "Departments fetched successfully!",
            Status = true,
            Data = departmentDtos
        };
    }
}