#nullable enable
using System;

namespace StudentManagementMVC.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledOn { get; set; } = DateTime.Now;

        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
