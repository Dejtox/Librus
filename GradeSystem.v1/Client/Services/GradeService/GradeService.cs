using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.GradeService
{
    public class GradeService : IGradeService
    {
        public GradeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Grade> Grades { get; set; } = new List<Grade>();
        public IList<Student> Students { get; set; } = new List<Student>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateGrade(Grade grade)
        {
            await _http.PostAsJsonAsync("api/Grades", grade);
            _navigationManager.NavigateTo("grades");
        }

        public async Task DeleteGrade(int id)
        {
            await _http.DeleteAsync($"api/Grades/{id}");
            _navigationManager.NavigateTo("grades");
        }

        public async Task<Grade> GetGradeByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Grade>($"api/Grades/{id}");
            if (result != null)
                return result;
            throw new Exception("Grade not found");
        }
        [Authorize(Roles = "Teacher")]
        public async Task GetGrades()
        {
            var result = await _http.GetFromJsonAsync<List<Grade>>("api/Grades");
            if (result != null)
                Grades = result;
        }

        public async Task GetStudents()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/Students");
            if (result != null)
                Students = result;
        }

        public async Task<Student> GetStudentByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Student>($"api/Students/{id}");
            if (result != null)
                return result;
            throw new Exception("Student not found");
        }


        public async Task GetSubjects()
        {
            var result = await _http.GetFromJsonAsync<List<Subject>>("api/Subjects");
            if (result != null)
                Subjects = result;
        }

        public async Task<Subject> GetSubjectByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Subject>($"api/Subjects/{id}");
            if (result != null)
                return result;
            throw new Exception("Subject not found");
        }

        public async Task UpdateGrade(Grade grade)
        {
            await _http.PutAsJsonAsync($"api/Grades/{grade.GradeID}", grade);
            _navigationManager.NavigateTo("grades");
        }
    }
}
