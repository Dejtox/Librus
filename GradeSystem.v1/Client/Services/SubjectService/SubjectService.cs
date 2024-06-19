using GradeSystem.v1.Client.  Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        public SubjectService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateSubject(Subject subject)
        {
            await _http.PostAsJsonAsync("api/Subjects", subject);
            _navigationManager.NavigateTo("Subjects");
        }

        public async Task DeleteSubject(int id)
        {
            await _http.DeleteAsync($"api/Subjects/{id}");
            _navigationManager.NavigateTo("Subjects");
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

        public async Task<Teacher> GetTeacherByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Teacher>($"api/Teachers/{id}");
            if (result != null)
                return result;
            throw new Exception("Teacher not found");
        }

        public async Task GetTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
            if (result != null)
                Teachers = result;
        }

        public async Task UpdateSubject(Subject subject)
        {
            await _http.PutAsJsonAsync($"api/Subjects/{subject.SubjectID}", subject);
            _navigationManager.NavigateTo("Subjects");
        }
    }
}
