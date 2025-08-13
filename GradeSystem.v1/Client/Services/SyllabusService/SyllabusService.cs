
using GradeSystem.v1.Client.Services.SyllabusService;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.SyllabusService
{
    public class SyllabusService : ISyllabusService
    {
        public SyllabusService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<Syllabus> Syllabuses { get; set; } = new List<Syllabus>();

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public async Task CreateSyllabus(Syllabus syllabus)
        {
            await _http.PostAsJsonAsync("api/Syllabus", syllabus);
        }

        public async Task DeleteSyllabus(int id)
        {
            await _http.DeleteAsync($"api/Syllabus/{id}");
        }

        public async Task<Syllabus> GetSyllabusByID(int id)
        {
            var result = await _http.GetFromJsonAsync<Syllabus>($"api/Syllabus/{id}");
            if (result != null)
                return result;
            throw new Exception("Syllabus not found");
        }
        
        public async Task GetSyllabuses()
        {
            var result = await _http.GetFromJsonAsync<List<Syllabus>>("api/Syllabus");
            if (result != null)
                Syllabuses = result;
        }





        public async Task UpdateSyllabus(Syllabus syllabus)
        {
            await _http.PutAsJsonAsync($"api/Syllabus/{syllabus.SyllabusID}", syllabus);
        }


    }
}
