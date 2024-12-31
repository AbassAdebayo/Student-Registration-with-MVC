namespace StudentRegistration.Entities;

public class Department : BaseEntity
{
    public string DepartmentName { get; set; }
    public string DepartmentCode { get; set; }
    
    public int? StudentCount { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}