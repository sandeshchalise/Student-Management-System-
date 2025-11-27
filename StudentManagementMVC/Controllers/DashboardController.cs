using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Models;
using StudentManagementMVC.Services;
using System.Threading.Tasks;

namespace StudentManagementMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardApiService _service;

        public DashboardController(DashboardApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = await _service.GetDashboardAsync();
            return View(dashboard);
        }
    }
}
