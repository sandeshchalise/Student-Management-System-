using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using StudentManagementMVC.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient<StudentApiService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:5000/"); 
});

builder.Services.AddHttpClient<CourseApiService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:5000/"); 
});

builder.Services.AddHttpClient<EnrollmentApiService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:5000/"); 
});
builder.Services.AddHttpClient<DashboardApiService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}"
);

app.Run();
