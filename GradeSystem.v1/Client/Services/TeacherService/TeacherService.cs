using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace GradeSystem.v1.Client.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        public TeacherService(HttpClient http)
        {
            _http = http;
        }
        public IList<Teacher> Teachers { get ; set ; } = new List<Teacher>();
        public IList<Teacher> AvailableTeachers { get; set ; } = new List<Teacher>();
        public IList<Teacher> UnavailableTeachers { get; set; } = new List<Teacher>();

        private readonly HttpClient _http;
        public async Task CreateTeacher(Teacher teacher)
        {
            await _http.PostAsJsonAsync("api/Teachers", teacher );
        }
        public async Task<Teacher?> CreateTeacherUser(Teacher teacher)
        {
            var response= await _http.PostAsJsonAsync("api/Teachers/teacher_user", teacher);
            if(response.IsSuccessStatusCode)
            {
                var teacherCreated = await response.Content.ReadFromJsonAsync<Teacher>();
                return teacherCreated;
            }
            return null;
        }
        public async Task DeleteTeacher(int id)
        {
            await _http.DeleteAsync($"api/Teachers/{id}");
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

        public async Task<List<Teacher>> GetTeacherss()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>($"api/Teachers");
            if (result != null)
                return result;
            throw new Exception("Teachers not found");
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            await _http.PutAsJsonAsync($"api/Teachers/{teacher.TeacherID}", teacher );
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
