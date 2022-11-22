using GradeSystem.v1.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.ExtraClassesService
{
    public class ExtraClassesService : IExtraClassesService
    {
        public ExtraClassesService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<ExtraClasses> ExtraClasses { get; set; } = new List<ExtraClasses>();
        public IList<Teacher> Teacher { get; set; } = new List<Teacher>();
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateExtraClasses(ExtraClasses extraClasses)
        {
            await _http.PostAsJsonAsync("api/ExtraClasses", extraClasses);
            _navigationManager.NavigateTo("ExtraClasses");
        }

        public async Task DeleteExtraClasses(int id)
        {
            await _http.DeleteAsync($"api/ExtraClasses/{id}");
            _navigationManager.NavigateTo("ExtraClasses");
        }

        public async Task GetExtraClasses()
        {
            var result = await _http.GetFromJsonAsync<List<ExtraClasses>>("api/ExtraClasses");
            if (result != null)
                ExtraClasses = result;
        }

        public async Task<ExtraClasses> GetExtraClassesByID(int id)
        {
            var result = await _http.GetFromJsonAsync<ExtraClasses>($"api/ExtraClasses/{id}");
            if (result != null)
                return result;
            throw new Exception("Extra Classes not found");
            
        }

        public async Task GetTeachers()
        {
            var result = await _http.GetFromJsonAsync<List<Teacher>>("api/Teachers");
            if (result != null)
                Teacher = result;
        }

        public async Task UpdateExtraClasses(ExtraClasses extraClasses)
        {
            await _http.PutAsJsonAsync($"api/ExtraClasses/{extraClasses.ExtraClassesID}", extraClasses);
            _navigationManager.NavigateTo("Teachers");
        }
    }
}
