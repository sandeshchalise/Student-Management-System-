using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context)
        {
            _context = context;
        }

   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
            
            if (!await _context.Students.AnyAsync(s => s.StudentId == enrollment.StudentId) ||
                !await _context.Courses.AnyAsync(c => c.Id == enrollment.CourseId))  // <- use Id
            {
                return BadRequest("Invalid StudentId or CourseId.");
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrollments), new
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId
            }, enrollment);
        }

        
        [HttpDelete("{studentId}/{courseId}")]
        public async Task<IActionResult> DeleteEnrollment(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
