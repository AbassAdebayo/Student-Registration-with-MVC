using Microsoft.EntityFrameworkCore;
using StudentRegistration.Entities;

namespace StudentRegistration.StudentDbContext;

public class StudentContext : DbContext
{
    public StudentContext(DbContextOptions<StudentContext> options) 
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
}