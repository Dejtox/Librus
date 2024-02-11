using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.EnrollmentService
{
    public class EnrollmentService : IEnrollmentService
    {
        public EnrollmentService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public IList<Class> Classes { get; set; } = new List<Class>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateEnrollment(Enrollment enrollment)
        {
            await _http.PostAsJsonAsync("api/Enrollments", enrollment);
            _navigationManager.NavigateTo("enrollments");
        }

        public async Task DeleteEnrollment(int id)
        {
            await _http.DeleteAsync($"api/Enrollments/{id}");
            _navigationManager.NavigateTo("enrollments");
        }

        public async Task<Class> GetClassByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Class>($"api/Classes/{id}");
            if (result != null)
                return result;
            throw new Exception("Class not found");
        }

        public async Task GetClasses()
        {
            var result = await _http.GetFromJsonAsync<List<Class>>("api/Classes");
            if (result != null)
                Classes = result;
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

        public async Task<Subject> GetSubjectByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Subject>($"api/Subjects/{id}");
            if (result != null)
                return result;
            throw new Exception("Subject not found");
        }

        public async Task GetSubjects()
        {
            var result = await _http.GetFromJsonAsync<List<Subject>>("api/Subjects");
            if (result != null)
                Subjects = result;
        }

        public async Task UpdateEnrollment(Enrollment enrollment)
        {
            await _http.PutAsJsonAsync($"api/Enrollments/{enrollment.EnrollmentID}", enrollment);
            _navigationManager.NavigateTo("enrollments");
        }
    }
}
