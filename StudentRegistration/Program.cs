using Microsoft.EntityFrameworkCore;
using StudentRegistration.Implementations.Repositories;
using StudentRegistration.Implementations.Services;
using StudentRegistration.Interfaces.Repositories;
using StudentRegistration.Interfaces.Services;
using StudentRegistration.StudentDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add Database
builder.Services.AddDbContext<StudentContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("StudentContext"),
        new MySqlServerVersion(new Version(7, 0, 22))
        ));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();