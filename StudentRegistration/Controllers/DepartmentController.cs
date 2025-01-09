using Microsoft.AspNetCore.Mvc;
using StudentRegistration.DTOs;
using StudentRegistration.Interfaces.Services;

namespace StudentRegistration.Controllers;

public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(_departmentService.GetAllDepartments());
    }
    
    [HttpGet]
    public IActionResult AddDepartment()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddDepartment(CreateDepartmentRequestModel model)
    {
        await _departmentService.CreateDepartment(model);
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> EditDepartment(Guid departmentId)
    {
        var department = await _departmentService.GetDepartmentById(departmentId);
        if (department == null)
        {
            throw new Exception("Request unsuccessful!");
        }

        return View();
    }

    [HttpPut]
    public async Task<IActionResult> EditDepartment(Guid departmentId, UpdateDepartmentRequestModel model)
    {
        await _departmentService.EditDepartment(departmentId, model);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteDepartment(Guid departmentId)
    {
        var department = await _departmentService.GetDepartmentById(departmentId);
        if (department == null)
        {
            throw new Exception("Request unsuccessful!");
        }

        return View();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteConfirmed(Guid departmentId)
    {
        await _departmentService.DeleteDepartment(departmentId);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartment(Guid departmentId)
    {
        var department = await _departmentService.GetDepartmentById(departmentId);
        if (department == null)
        {
            throw new Exception("Request unsuccessful!");
        }

        return View(department);
    }
    
}