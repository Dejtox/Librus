
using GradeSystem.v1.Client.Services.ClassService;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.ClassService
{
    public class ClassService : IClassService
    {
        public ClassService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Class> Classes { get; set; } = new List<Class>();
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public async Task CreateClass(Class classs)
        {
            await _http.PostAsJsonAsync("api/Classes", classs);
            _navigationManager.NavigateTo("Classes");
        }

        public async Task DeleteClass(int id)
        {
            await _http.DeleteAsync($"api/Classes/{id}");
            _navigationManager.NavigateTo("Classes");
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

        public async Task UpdateClass(Class classs)
        {
            await _http.PutAsJsonAsync($"api/Classes/{classs.ClassID}", classs);
            _navigationManager.NavigateTo("Classes");
        }
    }
}
