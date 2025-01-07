using StudentRegistration.DTOs;
using StudentRegistration.Entities;
using StudentRegistration.Interfaces.Repositories;
using StudentRegistration.Interfaces.Services;

namespace StudentRegistration.Implementations.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public StudentService(IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
    {
        _studentRepository = studentRepository;
        _departmentRepository = departmentRepository;
    }
    public async Task<BaseResponse<bool>> CreateStudent(Guid departmentId, CreateStudentRequestModel model)
    {
        var department = await _departmentRepository.GetDepartmentById(departmentId);
        if (department == null)
        {
            return new BaseResponse<bool>
            {
                Message = "Department not found!",
                Status = false
            };
        }
        
        var studentExist = await _studentRepository.StudentExistsByEmail(model.Email);
        if(studentExist)
        {
            return new BaseResponse<bool>
            {
                Message = "A student has already been registered with this email!",
                Status = false
            };
        }

        var student = new Student
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            FullName = $"{model.FirstName} {model.LastName} {model.MiddleName}",
            Email = model.Email,
            Address = model.Address,
            DepartmentId = departmentId,
            MatricNumber = await GenerateMatricNumber(departmentId),
            DateOfCreation = DateTime.UtcNow
        };
        var newStudent = await _studentRepository.CreateStudent(student);
        if (newStudent == null)
        {
            return new BaseResponse<bool>
            {
                Message = "Failed to register student!",
                Status = false
            };
        }

        return new BaseResponse<bool>
        {
            Message = "Student successfully registered...",
            Status = true
        };

    }

    public async Task<BaseResponse<bool>> StudentExistsByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<bool>> DeleteStudent(Guid studentId)
    {
        var student = await _studentRepository.GetStudentById(studentId);
        if (student == null)
        {
            return new BaseResponse<bool>
            {
                Message = "Student not found!",
                Status = false
            };
        }
        
        var deletedStudent = await _studentRepository.DeleteStudent(student);
        if (!deletedStudent)
        {
            return new BaseResponse<bool>
            {
                Message = "Student couldn't be deleted!",
                Status = false
            };
        }

        return new BaseResponse<bool>
        {
            Message = "Student successfully deleted!",
            Status = true
        };
    }

    public async Task<BaseResponse<Student>> EditStudent(Guid studentId, UpdateStudentRequestModel model)
    {
        var student = await _studentRepository.GetStudentById(studentId);
        if (student == null)
        {
            return new BaseResponse<Student>
            {
                Message = "Student not found!",
                Status = false
            };
        }
        
        student.FirstName = model.FirstName;
        student.LastName = model.LastName;
        student.MiddleName = model.MiddleName;
        student.Email = model.Email;
        student.Address = model.Address;
        student.PhoneNumber = model.PhoneNumber;
        student.FullName = $"{model.FirstName} {model.LastName} {model.MiddleName}";
        
        var updateStudent = await _studentRepository.EditStudent(student);
        if (updateStudent == null)
        {
            return new BaseResponse<Student>
            {
                Message = "Student couldn't be updated!",
                Status = false
            };
        }

        return new BaseResponse<Student>
        {
            Message = "Student successfully updated!",
            Status = true,
            Data = updateStudent
        };
    }

    public async Task<BaseResponse<StudentDto>> GetStudentById(Guid studentId)
    {
        var student = await _studentRepository.GetStudentById(studentId);
        if (student == null)
        {
            return new BaseResponse<StudentDto>
            {
                Message = "Student not found!",
                Status = false
            };
        }

        var studentDto = new StudentDto
        {
            Id = student.Id,
            FirstName = student.FullName,
            Email = student.Email,
            DepartmentId = student.DepartmentId,
            MatricNumber = student.MatricNumber,
            DateOfCreation = student.DateOfCreation,
        };
        return new BaseResponse<StudentDto>
        {
            Message = "Student successfully retrieved!",
            Status = true,
            Data = studentDto
        };
    }

    public async Task<BaseResponse<IList<StudentDto>>> GetAllStudents()
    {
        var students = await _studentRepository.GetAllStudents();
        if (students is null || !students.Any())
        {
            return new BaseResponse<IList<StudentDto>>
            {
                Message = "Students are empty!",
                Status = false
            };
        }

        var studentsDto = students.Select(std => new StudentDto
        {
            Id = std.Id,
            FirstName = std.FullName,
            Email = std.Email,
            DepartmentId = std.DepartmentId,
            MatricNumber = std.MatricNumber,
            DateOfCreation = std.DateOfCreation,

        }).ToList();

        return new BaseResponse<IList<StudentDto>>
        {
            Message = "Students successfully fetched!",
            Status = true,
            Data = studentsDto
        };

    }

    public async Task<BaseResponse<IList<StudentDto>>> GetStudentsByDepartment(Guid departmentId)
    {
        var studentsByDepartment = await _studentRepository.GetStudentsByDepartment(departmentId);
        if (studentsByDepartment is null || !studentsByDepartment.Any())
        {
            return new BaseResponse<IList<StudentDto>>
            {
                Message = "There are no students in this department!",
                Status = false
            };
        }

        var studentsByDepartmentDto = studentsByDepartment.Select(std => new StudentDto
        {
            Id = std.Id,
            FirstName = std.FullName,
            Email = std.Email,
            DepartmentId = std.DepartmentId,
            MatricNumber = std.MatricNumber,
            DateOfCreation = std.DateOfCreation,

        }).ToList();

        return new BaseResponse<IList<StudentDto>>
        {
            Message = "Students successfully fetched!",
            Status = true,
            Data = studentsByDepartmentDto
        };
    }

    private async Task<string> GenerateMatricNumber(Guid departmentId)
    {
        var department = await _departmentRepository.GetDepartmentById(departmentId);
        if (department == null) throw new ArgumentException("Department not found!");

        string departmentCode = department.DepartmentCode;
        string year = DateTime.Now.Year.ToString();
        int? count = department.StudentCount + 1;

        department.UpdateDepartmentStudentCount(count);
        
        await _departmentRepository.EditDepartment(department);

        return $"{departmentCode}{year}{count:D3}";

    }
}