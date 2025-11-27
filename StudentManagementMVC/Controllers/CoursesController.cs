using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Models;
using StudentManagementMVC.Services;
using System.Threading.Tasks;

namespace StudentManagementMVC.Controllers;

public class CoursesController : Controller
{
    private readonly CourseApiService _s;
    public CoursesController(CourseApiService s) { _s = s; }

    public async Task<IActionResult> Index() => View(await _s.GetAllAsync());
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Course c)
    {
        if (await _s.AddAsync(c)) return RedirectToAction("Index");
        return View(c);
    }

    public async Task<IActionResult> Edit(int id) => View(await _s.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Course c)
    {
        if (await _s.UpdateAsync(id, c)) return RedirectToAction("Index");
        return View(c);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _s.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}
