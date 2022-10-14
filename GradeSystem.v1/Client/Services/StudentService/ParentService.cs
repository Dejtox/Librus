using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.StudentService
{
    public class ParentService : IParentService
    {
        public ParentService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Parent> Parents { get; set; } = new List<Parent>();
        public IList<Student> Students { get; set; } = new List<Student>();
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateParent(Parent parent)
        {
            var result = await _http.PostAsJsonAsync("api/Parents", parent);
            await SetParents(result);
            
        }

        private async Task SetParents(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Parent>>();
            Parents = response;
            _navigationManager.NavigateTo("Parents");
        }

        public async Task DeleteParent(int id)
        {
            var result = await _http.DeleteAsync($"api/Parents/{id}");
            await SetParents(result);
        }

        public async Task<Parent> GetParentById(int id)
        {
            var result = await _http.GetFromJsonAsync<Parent>($"api/Parents/{id}");
            if (result != null)
                return result;
            throw new Exception("Parent no find");
        }

        public Task<Parent> GetParentByName(string name)
        {
            throw new NotImplementedException();
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

        public async Task UpdateParent(Parent parent)
        {
            var result = await _http.PutAsJsonAsync($"api/Parents/{parent.ParentID}", parent);
            await SetParents(result);
        }
    }
}
