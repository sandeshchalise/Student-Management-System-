using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using StudentManagementMVC.Models;

#nullable enable

namespace StudentManagementMVC.Services
{
    public class CourseApiService
    {
        private readonly HttpClient _http;

        public CourseApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            var response = await _http.GetAsync("api/courses");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Course>>(json) ?? new List<Course>();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"api/courses/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Course>(json);
        }

        public async Task<bool> AddAsync(Course course)
        {
            var json = JsonConvert.SerializeObject(course);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _http.PostAsync("api/courses", payload);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, Course course)
        {
            var json = JsonConvert.SerializeObject(course);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _http.PutAsync($"api/courses/{id}", payload);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _http.DeleteAsync($"api/courses/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
