using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Models;
using StudentManagementMVC.Services;
using System.Threading.Tasks;

namespace StudentManagementMVC.Controllers;

public class EnrollmentsController : Controller
{
    private readonly EnrollmentApiService _s;
    private readonly StudentApiService _students;
    private readonly CourseApiService _courses;

    public EnrollmentsController(EnrollmentApiService s, StudentApiService students, CourseApiService courses)
    {
        _s = s;
        _students = students;
        _courses = courses;
    }

    public async Task<IActionResult> Index() => View(await _s.GetAllAsync());

    public async Task<IActionResult> Create()
    {
        ViewData["Students"] = await _students.GetAllAsync();
        ViewData["Courses"] = await _courses.GetAllAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Enrollment e)
    {
        if (await _s.AddAsync(e)) return RedirectToAction("Index");

        ViewData["Students"] = await _students.GetAllAsync();
        ViewData["Courses"] = await _courses.GetAllAsync();
        return View(e);
    }

    public async Task<IActionResult> Delete(int studentId, int courseId)
    {
        await _s.DeleteAsync(studentId, courseId);
        return RedirectToAction("Index");
    }
}
