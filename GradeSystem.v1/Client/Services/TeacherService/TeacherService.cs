using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        public TeacherService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Teacher> Teachers { get ; set ; } = new List<Teacher>();
        public IList<Teacher> AvailableTeachers { get; set ; } = new List<Teacher>();
        public IList<Teacher> UnavailableTeachers { get; set; } = new List<Teacher>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public async Task CreateTeacher(Teacher teacher)
        {
            await _http.PostAsJsonAsync("api/Teachers", teacher );
            _navigationManager.NavigateTo("Teachers");
        }

        public async Task DeleteTeacher(int id)
        {
            await _http.DeleteAsync($"api/Teachers/{id}");
            _navigationManager.NavigateTo("Teachers");
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

        public async Task UpdateTeacher(Teacher teacher)
        {
            await _http.PutAsJsonAsync($"api/Teachers/{teacher.TeacherID}", teacher );
            _navigationManager.NavigateTo("Teachers");
        }

        public async Task GetAvailableTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers/available_teachers");
            if (result != null)
                AvailableTeachers = result;
        }

        public async Task GetUnavailableTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers/unavailable_teachers");
            if(result != null)
                UnavailableTeachers = result;
        }

        public async Task UpdateTeacherStatus()
        {
            await _http.PutAsync("api/Teachers/update_teacher_status",null);
        }
    }
}
