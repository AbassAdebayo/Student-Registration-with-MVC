using System.ComponentModel.DataAnnotations;
using StudentRegistration.Entities;

namespace StudentRegistration.DTOs;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string MatricNumber { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
    public string DepartmentName { get; set; }
    public DateTime DateOfCreation { get; set; }

    private string _fullName;
    public string FullName
    {
        get => _fullName?? $"{FirstName} {LastName} {MiddleName}"; 
        set => _fullName = value;
    }
}

public class CreateStudentRequestModel
{
    //public Guid DepartmentId { get; set; }
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string FirstName { get; set; }
    
    [Required]
     [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string MiddleName { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string Address { get; set; }
    
}

public class UpdateStudentRequestModel
{
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(maximumLength: 25, MinimumLength = 2)]
    public string MiddleName { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    
    [Required]
    public string Address { get; set; }
}