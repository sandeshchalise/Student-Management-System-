using System.Collections.Generic;

namespace StudentManagementMVC.Models
{
    public class DashboardViewModel
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
