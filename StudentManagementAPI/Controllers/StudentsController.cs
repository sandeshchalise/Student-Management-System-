using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _db.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();
            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Create(Student s)
        {
            _db.Students.Add(s);
            await _db.SaveChangesAsync();
            return Ok(s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student s)
        {
            var existing = await _db.Students.FindAsync(id);
            if (existing == null) return NotFound();

            existing.FirstName = s.FirstName;
            existing.LastName = s.LastName;
            existing.Email = s.Email;
            existing.DateOfBirth = s.DateOfBirth;

            await _db.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();

            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
