namespace StudentRegistration.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string MatricNumber { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
    
}