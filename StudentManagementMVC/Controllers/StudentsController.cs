using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Models;
using StudentManagementMVC.Services;
using System.Threading.Tasks;

namespace StudentManagementMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentApiService _studentsService;
        private readonly CourseApiService _coursesService;
        private readonly EnrollmentApiService _enrollmentService;

        public StudentsController(
            StudentApiService studentsService,
            CourseApiService coursesService,
            EnrollmentApiService enrollmentService)
        {
            _studentsService = studentsService;
            _coursesService = coursesService;
            _enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentsService.GetAllAsync();
            var courses = await _coursesService.GetAllAsync();
            var enrollments = await _enrollmentService.GetAllAsync();

            var vm = new DashboardViewModel
            {
                Students = students,
                Courses = courses,
                Enrollments = enrollments
            };

            return View(vm);
        }
    }
}
