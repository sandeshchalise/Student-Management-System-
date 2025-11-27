using StudentManagementMVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StudentManagementMVC.Services
{
    public class DashboardApiService
    {
        private readonly HttpClient _http;

        public DashboardApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var students = await _http.GetFromJsonAsync<List<Student>>("https://localhost:5001/api/Students");
            var courses = await _http.GetFromJsonAsync<List<Course>>("https://localhost:5001/api/Courses");
            var enrollments = await _http.GetFromJsonAsync<List<Enrollment>>("https://localhost:5001/api/Enrollments");

            return new DashboardViewModel
            {
                Students = students ?? new List<Student>(),
                Courses = courses ?? new List<Course>(),
                Enrollments = enrollments ?? new List<Enrollment>()
            };
        }
    }
}
