using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentRegistration.DTOs;
using StudentRegistration.Entities;
using StudentRegistration.Interfaces.Services;

namespace StudentRegistration.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly IDepartmentService _departmentService;

    public StudentController(IStudentService studentService, IDepartmentService departmentService)
    {
        _studentService = studentService;
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(_studentService.GetAllStudents());
    }
    
    [HttpGet]
    public async Task<IActionResult> AddStudent()
    {
        var departmentsResponse = await _departmentService.GetAllDepartments();
        
            var departments = departmentsResponse.Data ?? new List<DepartmentDto>();
            ViewData["Departments"] = departments.Any()
                ? new SelectList(departments, "Id", "DepartmentName")
                : new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "No departments available", Disabled = true}
                });   
        

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent(Guid departmentId, CreateStudentRequestModel model)
    {
        await _studentService.CreateStudent(departmentId, model);
        return RedirectToAction("GetAllStudentsByDepartment");
    }
    
    [HttpGet]
    public async Task<IActionResult> EditStudent(Guid studentId)
    {
        var student = await _studentService.GetStudentById(studentId);
        if (student == null)
        {
            throw new Exception("Request unsuccessful!");
        }

        return View();
    }

    [HttpPut]
    public async Task<IActionResult> EditStudent(Guid studentId, UpdateStudentRequestModel model)
    {
        await _studentService.EditStudent(studentId, model);
        
        return RedirectToAction("GetAllStudentsByDepartment");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteStudent(Guid studentId)
    {
        var student = await _studentService.GetStudentById(studentId);
        if (student == null)
        {
            throw new Exception("Request unsuccessful!");
        }

        return View();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteConfirmed(Guid studentId)
    {
        await _studentService.DeleteStudent(studentId);
        return RedirectToAction("GetAllStudentsByDepartment");
    }

    [HttpGet]
    public async Task<IActionResult> GetStudent(Guid studentId)
    {
        var student = await _studentService.GetStudentById(studentId);
        if (student == null)
        {
            throw new Exception("Request unsuccessful!");
        }

        return View(student);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllStudentsByDepartment(Guid departmentId)
    {
        return View(_studentService.GetStudentsByDepartment(departmentId));
    }

}