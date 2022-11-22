using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Client.Services.ExtraClassesListService
{

    public class ExtraClassesListService : IExtraClassesListService
    {
        public ExtraClassesListService(HttpClient http, NavigationManager navigationManager)
        {
                _http = http;
                _navigationManager = navigationManager;
            
        }

        private readonly HttpClient _http;
        
        private readonly NavigationManager _navigationManager;


        public IList<ExtraClassesList> ExtraClassesLists{ get; set; } = new List<ExtraClassesList>();
        public IList<Student> Students { get; set; } = new List<Student>();
        public IList<ExtraClasses> ExtraClasses { get; set; } = new List<ExtraClasses>();

        public async Task CreateExtraClassesList(ExtraClassesList extraClassesList)
        {
            await _http.PostAsJsonAsync("api/ExtraClassesLists", extraClassesList);
            _navigationManager.NavigateTo("ExtraClassesList");
        }

        public async Task DeleteExtraClassesList(int id)
        {
            await _http.DeleteAsync( $"api/ExtraClassesLists/{id}");
            _navigationManager.NavigateTo("ExtraClassesList");
        }

        public async Task GetExtraClasses()
        {
            var result=await _http.GetFromJsonAsync<List<ExtraClasses>>("api/ExtraClasses");
            if(result!=null)
            {
                ExtraClasses = result;
            }
        }

        public async Task GetExtraClassesList()
        {
            var result = await _http.GetFromJsonAsync<List<ExtraClassesList>>("api/ExtraClassesLists");
            if (result != null)
            {
                ExtraClassesLists = result;
            }
        }

        public async Task<ExtraClassesList> GetExtraClassesListByID(int id)
        {
            var result = await _http.GetFromJsonAsync<ExtraClassesList>($"api/ExtraClassesLists/{id}");
            if (result != null)
            {
               return result;
            }
            throw new Exception("Extra Classes List not found");
        }

        public async Task GetStudemts()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/Students");
            if (result != null)
            {
                Students = result;
            }
        }

        public async Task UpdateExtraClassesList(ExtraClassesList extraClassesList)
        {
            await _http.PutAsJsonAsync($"api/ExtraClassesLists{extraClassesList.ExtraClassesListID}", extraClassesList);
            _navigationManager.NavigateTo("ExtraClassesList");
        }
    }
}
