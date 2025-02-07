using Microsoft.AspNetCore.Mvc;
using StudentRegistration.DTOs;
using StudentRegistration.Interfaces.Services;
using StudentRegistration.Models;

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
        var response = await _departmentService.GetAllDepartments();
            
        if(!response.Status) return View("Error", new ErrorViewModel
        {
            Message  = response.Message
        });
        return View(response.Data);
    }
    
    [HttpGet]
    public IActionResult AddDepartment()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddDepartment(CreateDepartmentRequestModel model)
    {
        var response = await _departmentService.CreateDepartment(model);
        if(!response.Status) return View("Error", new ErrorViewModel
        {
          Message  = response.Message
        });
        
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> EditDepartment(Guid departmentId)
    {
        var response = await _departmentService.GetDepartmentById(departmentId);
        if(!response.Status) return View("Error", new ErrorViewModel
        {
            Message  = response.Message
        });

        return View();
    }

    [HttpPut]
    public async Task<IActionResult> EditDepartment(Guid departmentId, UpdateDepartmentRequestModel model)
    {
        var response = await _departmentService.EditDepartment(departmentId, model);
        
        if(!response.Status) return View("Error", new ErrorViewModel
        {
            Message  = response.Message
        });
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteDepartment(Guid departmentId)
    {
        var response = await _departmentService.GetDepartmentById(departmentId);
        if(!response.Status) return View("Error", new ErrorViewModel
            
        {
            Message  = response.Message
        });

        return View();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteConfirmed(Guid departmentId)
    {
        var response = await _departmentService.DeleteDepartment(departmentId);
        
        if(!response.Status) return View("Error", new ErrorViewModel
        {
            Message  = response.Message
        });
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartment(Guid departmentId)
    {
        var response = await _departmentService.GetDepartmentById(departmentId);
        
        if(!response.Status) return View("Error", new ErrorViewModel
        {
            Message = response.Message
        });

        return View(response.Data);
    }
    
}