using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.UserService
{
    public class UserService : IUserService
    {
        public IList<User> Users { get; set; } = new List<User>();
        public IList<Teacher> Teachers { get; set; } = new List<Teacher>();
        public IList<Student> Students { get;set; } = new List<Student>();
        public IList<Parent> Parents { get; set; } = new List<Parent>();
        private readonly HttpClient _http;
        public UserService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
        }
        public async Task CreateUser(User user)
        {
            await _http.PostAsJsonAsync("api/Users", user);
            //_navigationManager.NavigateTo("Teachers");
        }

        public async Task DeleteUser(int id)
        {
            await _http.DeleteAsync($"api/Users/{id}");
            //_navigationManager.NavigateTo("Teachers");
        }

        public async Task GetParents()
        {
            var result = await _http.GetFromJsonAsync<List<Parent>>("api/Parents");
            if (result != null)
                Parents = result;
        }

        public async Task GetStudents()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/Students");
            if (result != null)
                Students = result;
        }

        public async Task GetTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
            if (result != null)
                Teachers = result;
        }

        public async Task<User> GetUserByID(int id)
        {
            var result = await _http.GetFromJsonAsync<User>($"api/Users/{id}");
            if (result != null)
                return result;
            throw new Exception("User not found");
        }

        public async Task GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<User>>("api/Users");
            if (result != null)
                Users = result;
        }

        public async Task UpdateUser(User user)
        {
            await _http.PutAsJsonAsync($"api/Users/{user.UserID}", user);
            //_navigationManager.NavigateTo("Teachers");
        }

        public async Task<User> GetUserByLogin(string login)
        {
            var result = await _http.GetFromJsonAsync<User>($"api/Teachers/{login}");
            if (result != null)
                return result;
            throw new Exception("User not found");
        }
    }
}
