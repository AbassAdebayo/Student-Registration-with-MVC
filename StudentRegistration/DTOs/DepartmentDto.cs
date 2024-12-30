using System.ComponentModel.DataAnnotations;
using StudentRegistration.Entities;

namespace StudentRegistration.DTOs;

public class DepartmentDto
{
    public string DepartmentName { get; set; }
    public string DepartmentCode { get; set; }
    public ICollection<StudentDto> Students { get; set; } = new List<StudentDto>();
}

public class CreateDepartmentRequestModel
{
    [Required]
    public string DepartmentName { get; set; }
    
    [Required]
    [StringLength(maximumLength: 5, MinimumLength = 3)]
    public string DepartmentCode { get; set; }
}

public class UpdateDepartmentRequestModel
{
    [Required]
    public string DepartmentName { get; set; }
    
    [Required]
    [StringLength(maximumLength: 5, MinimumLength = 3)]
    public string DepartmentCode { get; set; }
}