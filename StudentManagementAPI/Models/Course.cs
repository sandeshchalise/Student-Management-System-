using System;
using System.Collections.Generic;

namespace StudentManagementAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
       public List<Enrollment>? Enrollments { get; set; }
        
    }
}
