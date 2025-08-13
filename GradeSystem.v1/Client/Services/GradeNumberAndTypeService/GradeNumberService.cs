
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;


namespace GradeSystem.v1.Client.Services.GradeNumberService
{
    public class GradeNumberService : IGradeNumberService
    {
        public GradeNumberService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public IList<GradeNumber> GradeNumbers { get; set; } = new List<GradeNumber>();
        public IList<GradeType> GradeTypes { get; set; } = new List<GradeType>();
     

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public async Task DeleteGradeNumber(int id)
        {
            await _http.DeleteAsync($"api/Gradenumber/{id}");
        }
        public async Task DeleteGradeType(int id)
        {
            await _http.DeleteAsync($"api/Gradetype/{id}");
        }

        public async Task<GradeNumber> GetGradeNumberById(int id)
        {
            var result = await _http.GetFromJsonAsync<GradeNumber>($"api/Gradenumber/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("BookType Not Find");

        }
        public async Task<GradeType> GetGradeTypeById(int id)
        {
            var result = await _http.GetFromJsonAsync<GradeType>($"api/Gradetype/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("BookType Not Find");

        }

        public async Task GetGradeNumbers()
        {
            var result = await _http.GetFromJsonAsync<List<GradeNumber>>("api/Gradenumber");
            if (result != null)
                GradeNumbers = result;
        }

        public async Task PostGradeNumber(GradeNumber gradenumber)
        {
            await _http.PostAsJsonAsync("api/Gradenumber", gradenumber);
        }

        public async Task PutGradeNumber(int id, GradeNumber gradenumber)
        {
            await _http.PutAsJsonAsync($"api/Gradenumber/{id}", gradenumber);
        }

        public async Task PostGradeType(GradeType gradeType)
        {
            await _http.PostAsJsonAsync("api/GradeType", gradeType);
        }

        public async Task PutGradeType(int id, GradeType gradeType)
        {
            await _http.PutAsJsonAsync($"api/GradeType/{id}", gradeType);
        }

        public async Task GetGradeTypes()
        {
            var result = await _http.GetFromJsonAsync<List<GradeType>>("api/GradeType");
            if (result != null)
                GradeTypes = result;
        }
    }
}
