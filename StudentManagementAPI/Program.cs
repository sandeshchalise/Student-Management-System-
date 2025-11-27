using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("StudentDb"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Students.Any())
    {
        db.Students.AddRange(
            new Student { FirstName = "Sandesh", LastName = "Chalise", Email = "sandesh123@gmail.com", DateOfBirth = new DateTime(2000, 1, 1) },
            new Student { FirstName = "Ram", LastName = "Karki", Email = "ram@gmail.com", DateOfBirth = new DateTime(1999, 5, 15) }
        );
        db.SaveChanges();
    }

    if (!db.Courses.Any())
    {
        db.Courses.AddRange(
            new Course { Title = "C# Basics" },
            new Course { Title = "ASP.NET Core" },
            new Course { Title = "Entity Framework Core" }
        );
        db.SaveChanges();
    }

    if (!db.Enrollments.Any())
    {
        db.Enrollments.AddRange(
            new Enrollment { StudentId = 1, CourseId = 1 },
            new Enrollment { StudentId = 1, CourseId = 2 },
            new Enrollment { StudentId = 2, CourseId = 1 },
            new Enrollment { StudentId = 2, CourseId = 3 }
        );
        db.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentManagementAPI v1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthorization();
app.MapControllers();
app.Run();
