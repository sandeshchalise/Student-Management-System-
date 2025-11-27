using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using StudentManagementMVC.Models;

#nullable enable

namespace StudentManagementMVC.Services
{
    public class EnrollmentApiService
    {
        private readonly HttpClient _http;

        public EnrollmentApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Enrollment>> GetAllAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/enrollments");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Enrollment>>(json) ?? new List<Enrollment>();
            }
            catch
            {
                return new List<Enrollment>();
            }
        }

        public async Task<bool> AddAsync(Enrollment enrollment)
        {
            try
            {
                var json = JsonConvert.SerializeObject(enrollment);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await _http.PostAsync("api/enrollments", payload);
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int studentId, int courseId)
        {
            try
            {
                var result = await _http.DeleteAsync($"api/enrollments/{studentId}/{courseId}");
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
