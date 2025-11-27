using System.Collections.Generic;

namespace StudentManagementAPI.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }



        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
