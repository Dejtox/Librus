
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.LessonsHoursService
{
    public class LessonsHoursService : ILessonsHoursService
    {
        public LessonsHoursService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<LessonsHours> LessonsHours { get; set; } = new List<LessonsHours>();
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task CreateLessonsHours(LessonsHours lessonsHours)
        {
            await _http.PostAsJsonAsync("api/LessonsHours", lessonsHours);
            _navigationManager.NavigateTo("plan");
        }

        public async Task DeleteLessonsHours(int id)
        {
            await _http.DeleteAsync($"api/LessonsHours/{id}");
            _navigationManager.NavigateTo("plan");
        }

        public async Task GetLessonsHours()
        {
            var result = await _http.GetFromJsonAsync<List<LessonsHours>>("api/LessonsHours");
            if (result != null)
                LessonsHours = result;
        }

        public async Task<LessonsHours> GetLessonsHoursByID(int id)
        {
            var result = await _http.GetFromJsonAsync<LessonsHours>($"api/LessonsHours/{id}");
            if (result != null)
                return result;
            throw new Exception("Extra Classes not found");
            
        }

        public async Task UpdateLessonsHours(LessonsHours lessonsHours)
        {
            await _http.PutAsJsonAsync($"api/LessonsHours/{lessonsHours.ID}", lessonsHours);
            _navigationManager.NavigateTo("plan");
        }
    }
}
