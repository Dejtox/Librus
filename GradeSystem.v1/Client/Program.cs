global using GradeSystem.v1.Client.Services.StudentService;
global using GradeSystem.v1.Client.Services.ParentService;
global using GradeSystem.v1.Client.Services.TeacherService;
global using GradeSystem.v1.Client.Services.ClassService;
global using GradeSystem.v1.Client.Services.SubjectService;
global using GradeSystem.v1.Client.Services.EnrollmentService;
global using GradeSystem.v1.Client.Services.AttendanceService;
global using GradeSystem.v1.Client.Services.GradeService;
global using GradeSystem.v1.Client.Services.UserService;
global using GradeSystem.v1.Client.Services.ExtraClassesService;
global using GradeSystem.v1.Client.Services.ExtraClassesListService;
global using GradeSystem.v1.Client.Services.LessonsHoursService;
global using Microsoft.AspNetCore.Components.Authorization;
global using GradeSystem.v1.Client.Services.EventService;
global using GradeSystem.v1.Client.Services.BookService;
using GradeSystem.v1.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.SessionStorage;
using System.Reflection;
using GradeSystem.v1.Client.Auth;
using Syncfusion.Blazor;


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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExtraClassesService, ExtraClassesService>();
builder.Services.AddScoped<IExtraClassesListService, ExtraClassesListService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILessonsHoursService, LessonsHoursService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorizationCore();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddAuthentication();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzgxOTMzQDMyMzAyZTMzMmUzMFR3bzhDc3YxcVUvSE5zMUQvRFgva0xibmR2WjdjYlU2UkNFZXlGeHpRWUE9");





await builder.Build().RunAsync();
