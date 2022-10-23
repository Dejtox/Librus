using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.AttendanceService
{
    public class AttendanceService : IAttendanceService
    {
        public AttendanceService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Attendance> Attendances { get; set; } = new List<Attendance>();
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public IList<Student> Students { get; set; } = new List<Student>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateAttendance(Attendance attendance)
        {
            await _http.PostAsJsonAsync("api/Attendances", attendance);
            _navigationManager.NavigateTo("attendances");
        }

        public async Task DeleteAttendance(int id)
        {
            await _http.DeleteAsync($"api/Attendances/{id}");
            _navigationManager.NavigateTo("attendances");
        }

        public async Task<Attendance> GetAttendanceByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Attendance>($"api/Attendances/{id}");
            if (result != null)
                return result;
            throw new Exception("Attendance not found");
        }

        public async Task GetAttendances()
        {
            var result = await _http.GetFromJsonAsync<List<Attendance>>("api/Attendances");
            if (result != null)
                Attendances = result;
        }

        public async Task<Enrollment> GetEnrollmentByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Enrollment>($"api/Enrollments/{id}");
            if (result != null)
                return result;
            throw new Exception("Enrollment not found");
        }

        public async Task GetEnrollments()
        {
            var result = await _http.GetFromJsonAsync<List<Enrollment>>("api/Enrollments");
            if (result != null)
                Enrollments = result;
        }

        public async Task<Student> GetStudentByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Student>($"api/Students/{id}");
            if (result != null)
                return result;
            throw new Exception("Student not found");
        }

        public async Task GetStudents()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/Students");
            if (result != null)
                Students = result;
        }

        public async Task UpdateAttendance(Attendance attendance)
        {
            await _http.PutAsJsonAsync($"api/Attendances/{attendance.AttendanceID}", attendance);
            _navigationManager.NavigateTo("attendances");
        }
    }
}
