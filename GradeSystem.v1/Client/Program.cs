global using GradeSystem.v1.Client.Services.StudentService;
global using GradeSystem.v1.Client.Services.ParentService;
global using GradeSystem.v1.Client.Services.TeacherService;
global using GradeSystem.v1.Client.Services.ClassService;
global using GradeSystem.v1.Client.Services.SubjectService;
global using GradeSystem.v1.Client.Services.EnrollmentService;
global using GradeSystem.v1.Client.Services.AttendanceService;
global using GradeSystem.v1.Client.Services.GradeService;
using GradeSystem.v1.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GradeSystem.v1.Client.Services.Nowy_folder;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IGradeService, GradeService>();

await builder.Build().RunAsync();
