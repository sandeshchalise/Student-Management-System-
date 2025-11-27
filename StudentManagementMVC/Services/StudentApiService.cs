using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StudentManagementMVC.Models;

#nullable enable  // Enable nullable reference types

namespace StudentManagementMVC.Services
{
    public class StudentApiService
    {
        private readonly HttpClient _http;

        public StudentApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var response = await _http.GetAsync("api/students");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"api/students/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Student>(json);
        }

        public async Task<bool> AddAsync(Student student)
        {
            var json = JsonConvert.SerializeObject(student);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _http.PostAsync("api/students", payload);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, Student student)
        {
            var json = JsonConvert.SerializeObject(student);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _http.PutAsync($"api/students/{id}", payload);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _http.DeleteAsync($"api/students/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}

